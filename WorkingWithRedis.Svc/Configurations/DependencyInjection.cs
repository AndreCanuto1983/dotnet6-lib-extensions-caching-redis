using Application.Interfaces;
using Application.Repositories;

namespace WorkingWithRedis.Configurations
{
    public class DependencyInjection
    {
        public static void Configurations(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();            
        }
    }
}
