using api.Context;
using api.DTO.User;
using api.Exeption;
using api.Model;
using api.VOs.User;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace api.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly JWTService _jwtService;
        private readonly IMapper _mapper;
        public UserService(AppDbContext context, JWTService jwtService, IMapper mapper)
        {
            _context = context;
            _jwtService = jwtService;
            _mapper = mapper;
        }
        public async Task <string> Auth(AuthDTO auth)
        {
            var verify = await _context.Users.Where(x => x.Email.ToLower().Equals(auth.Email) && x.Password.Equals(auth.Password)).FirstOrDefaultAsync();
                if (verify == null) throw new AplicationRequestExeption("Usuario não encontrado",HttpStatusCode.NotFound);
            var Token = _jwtService.GerarToken(verify);
            return Token;
        }
        public async Task<string> Register(UserVOEnter user)
        {
            var verify = await _context.Users.Where(x => x.Email.ToLower().Equals(user.Email)).FirstOrDefaultAsync();
                if(verify != null) throw new AplicationRequestExeption("Usuario já registrado", HttpStatusCode.Unauthorized);

            var model = _mapper.Map<UserVOEnter, User>(user);
            await _context.Users.AddAsync(model);
            await _context.SaveChangesAsync();  

            var Token = _jwtService.GerarToken(model);
            return Token;
        }
        public string TestToken(HttpContext httpContext)
        {
            var id = int.Parse(httpContext.Items["Id"]!.ToString()!);
            var request = $"Seu id de usuario é: {id}";
            return request;
        }
    }
}
