using api.Context;
using api.DTO.ForgotPassword;
using api.Exeption;
using api.Model;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace api.Services
{
    public class ForgotPassowordService
    {
        private readonly AppDbContext _context;

        public ForgotPassowordService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateForgot(ForgotDTO forgot)
        {
            var verify = await _context.Users.Where(x => x.Email.ToLower().Equals(forgot.Email)).FirstOrDefaultAsync()
                ?? throw new AplicationRequestExeption("Email não cadastrado", HttpStatusCode.NotFound);
            var vefiryRecovery = await _context.ForgotPasswords.Where(x => x.Email == forgot.Email && x.isEnabled).FirstOrDefaultAsync();

            var Model = new ForgotPassword();
            if (vefiryRecovery == null)
            {
                Model = new ForgotPassword() { Email = forgot.Email };
                await _context.ForgotPasswords.AddAsync(Model);
                await _context.SaveChangesAsync();
            }
            else
            {
                Model = vefiryRecovery;
            }
            return;

        }
        public async Task RecoveryPassword(RecoveryDTO recovery)
        {
            var verify = await _context.ForgotPasswords.Where(x => x.Id.Equals(recovery.Id)).FirstOrDefaultAsync()
                ?? throw new AplicationRequestExeption("Recovery de senha não cadastrado", HttpStatusCode.NotFound);
            if(!verify.isEnabled) throw new AplicationRequestExeption("Esse link não é mais valido.", HttpStatusCode.NotFound);

            var getEntity = await _context.Users.FirstOrDefaultAsync(x => x.Email == verify.Email) 
                ?? throw new AplicationRequestExeption("Erro Interno", HttpStatusCode.NotFound);

            getEntity.Password = recovery.Password;
            verify.isEnabled = false;
            _context.Entry(getEntity).State = EntityState.Modified;
            _context.Entry(verify).State = EntityState.Modified;
            await _context.SaveChangesAsync();  

            return;

        }
        public async Task VerifyRecovery(Guid id)
        {
            var verify = await _context.ForgotPasswords.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync()
                ?? throw new AplicationRequestExeption("Recovery de senha não cadastrado", HttpStatusCode.NotFound);

            if(!verify.isEnabled) throw new AplicationRequestExeption("Recovery não é mais valido", HttpStatusCode.NotFound);
            return;
        }
    }
}
