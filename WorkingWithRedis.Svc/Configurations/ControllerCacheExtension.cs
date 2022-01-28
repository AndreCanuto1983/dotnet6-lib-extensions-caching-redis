using Microsoft.AspNetCore.Mvc;

namespace WorkingWithRedis.Svc.Configurations
{
    public static class ControllerCacheExtension
    {
        public static void ControllerCacheSettings(this IServiceCollection services)
        {
            services.AddResponseCaching();

            services.AddControllers(options =>
            {
                options.CacheProfiles.Add("Default900",
                    new CacheProfile()
                    {
                        Duration = 900
                    });
            });
        }
    }
}
