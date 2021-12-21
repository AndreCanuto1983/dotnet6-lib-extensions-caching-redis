using Application.Models.Configurations;

namespace WorkingWithRedis.Configurations
{
    public static class Redis
    {
        public static void Configurations(WebApplicationBuilder builder)
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
