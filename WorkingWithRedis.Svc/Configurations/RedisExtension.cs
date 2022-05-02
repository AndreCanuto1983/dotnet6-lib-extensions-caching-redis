﻿using Application.Models.Configurations;
using StackExchange.Redis;

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
                options.ConfigurationOptions = new ConfigurationOptions()
                {
                    ResponseTimeout = 10,
                    ConnectRetry = 3
                };
            });
        }
    }
}
