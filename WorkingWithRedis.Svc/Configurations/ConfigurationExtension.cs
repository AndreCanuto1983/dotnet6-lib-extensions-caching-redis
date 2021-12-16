﻿using System.Text.Json.Serialization;

namespace WorkingWithRedis.Configurations
{
    public static class ConfigurationExtension
    {
        public static void Configurations(IServiceCollection services)
        {
            services.AddControllers()
                    .AddJsonOptions(options =>
                        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true)
                    .AddJsonOptions(options =>
                        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault);
        }
    }
}
