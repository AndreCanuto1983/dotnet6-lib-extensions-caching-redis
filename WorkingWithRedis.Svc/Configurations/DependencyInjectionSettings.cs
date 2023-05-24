using Application.Interfaces;
using Application.Repositories;

namespace WorkingWithRedis.Configurations
{
    public static class DependencyInjectionSettings
    {
        public static void DependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();            
        }
    }
}
