using System.Text.Json.Serialization;

namespace WorkingWithRedis.Configurations
{
    public static class JsonSettings
    {
        public static void ConfigureJson(this IServiceCollection services)
        {
            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
                    });
        }
    }
}
