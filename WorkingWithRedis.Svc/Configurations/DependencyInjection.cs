using Application.Interfaces;
using Application.Repositories;

namespace WorkingWithRedis.Configurations
{
    public static class DependencyInjection
    {
        public static void DependencyInjectionSettings(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();            
        }
    }
}
