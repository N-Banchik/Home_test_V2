using Home_test_V1.Middlewares;
using Home_test_V1.Services.Interfaces;
using Microsoft.AspNetCore.Builder;

namespace Home_test_V1.Startup
{
    /// <summary>
    /// Configuration for application middleware.
    /// </summary>
    public static class MiddlewareConfig
    {
        /// <summary>
        /// Adds middleware components to the application pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public static void AddMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            
        }
    }
}
