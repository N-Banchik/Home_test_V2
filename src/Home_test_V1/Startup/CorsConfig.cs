using Home_test_V1.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Home_test_V1.Startup
{
    /// <summary>
    /// CORS configuration.
    /// </summary>
    public static class CorsConfig
    {
        /// <summary>
        /// Adds CORS middleware to the application builder.
        /// </summary>
        /// <param name="app"></param>
        public static void AddCors(this IApplicationBuilder app)
        {
            // Enable CORS for all origins, methods, and headers
            app.UseCors(p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());

        }
    }
}
