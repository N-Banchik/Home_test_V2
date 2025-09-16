using Home_test_V1.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Home_test_V1.Startup
{
    /// <summary>
    /// Configuration for application services.
    /// </summary>
    public static class ServicesConfig
    {
        /// <summary>
        /// Adds application services to the DI container.
        /// </summary>
        /// <param name="services">The service collection to add services to.</param>
        /// <param name="configuration">The application configuration.</param>
        public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuration settings as app settings and JWT settings objects for DI
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            // Controllers
            services.AddControllers();

            // Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }
    }

}
