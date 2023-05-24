using Microsoft.AspNetCore.Mvc;

namespace WorkingWithRedis.Svc.Configurations
{
    public static class ControllerCacheSettings
    {
        public static void ControllerCache(this IServiceCollection services)
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
