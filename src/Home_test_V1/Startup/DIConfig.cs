using Home_test_V1.Services;
using Home_test_V1.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Home_test_V1.Startup
{
    /// <summary>
    /// Dependency Injection configuration.
    /// </summary>
    public static class DIConfig
    {
        /// <summary>
        /// Registers application services for dependency injection.
        /// </summary>
        /// <param name="services">The service collection to add services to.</param>
        public static void RegisterDependencies(this IServiceCollection services)
        {
            //Register MathOpService as Scoped lifetime
            services.AddScoped<IMathOpService, MathOpService>();
            //Register ErrorHandlingService as Transient lifetime
            services.AddTransient<IErrorHandlingService, ErrorHandlingService>();
        }
    }
}
