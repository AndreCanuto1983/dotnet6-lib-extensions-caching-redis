using Application.Models.Configurations;

namespace WorkingWithRedis.Configurations
{
    public static class RedisExtension
    {
        public static void RedisSettings(this WebApplicationBuilder builder)
        {
            var section = builder.Configuration.GetSection("RedisSettings");
            var configurations = section.Get<RedisSettings>();

            builder.Services.AddDistributedRedisCache(options =>
            {
                options.InstanceName = configurations.Instance;
                options.Configuration = configurations.Connection;
            });
        }
    }
}
