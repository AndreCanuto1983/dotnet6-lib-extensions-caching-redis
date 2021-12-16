using Application.Interfaces;
using Application.Repositories;

namespace WorkingWithRedis.Configurations
{
    public class ConfigurateDependencyInjection
    {
        public static void Configurations(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();            
        }
    }
}
