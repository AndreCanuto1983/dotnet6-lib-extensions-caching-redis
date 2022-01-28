using System.Text.Json.Serialization;

namespace WorkingWithRedis.Configurations
{
    public static class ServiceExtension
    {
        public static void ServiceExtensionSettings(this IServiceCollection services)
        {
            services.AddControllers()
                    .AddJsonOptions(options =>
                        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true)
                    .AddJsonOptions(options =>
                        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault);            
        }
    }
}
