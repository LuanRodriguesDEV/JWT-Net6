using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        public DbSet<User> Users { get; set; }
        public DbSet<ForgotPassword> ForgotPasswords { get; set; }
    }
}
