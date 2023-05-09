using api.Context;
using Microsoft.EntityFrameworkCore;

namespace api.Programs
{
    public static class DBSetup
    {
        public static void DbConnectionSetup(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("ApiConnectorString");
            if (connectionString != null)
                services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
        }
    }
}
