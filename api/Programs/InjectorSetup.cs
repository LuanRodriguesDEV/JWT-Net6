using api.Services;

namespace api.Programs
{
    public static class InjectorSetup
    {
        public static void AddInjections(this IServiceCollection services)
        {
            services.AddScoped<JWTService>();
            services.AddScoped<UserService>();
        }
    }
}
