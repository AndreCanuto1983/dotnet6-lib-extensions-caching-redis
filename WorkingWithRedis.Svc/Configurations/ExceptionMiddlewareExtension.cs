using WorkingWithRedis.Configurations.Services;

namespace WorkingWithRedis.Configurations
{
    public static class ExceptionMiddlewareExtension
    {
        public static void UseCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
