using Home_test_V1.Startup;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Home_test_V1
{
    /// <summary>
    /// Program
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configurations
            builder.Services.AddAppServices(builder.Configuration);
            builder.Services.RegisterDependencies();

            // Custom Bearer Authentication implemented in BearerAuthenticationHandler
            builder.Services.AddAuthentication(BearerAuthenticationHandler.SchemeName)
                .AddScheme<AuthenticationSchemeOptions, BearerAuthenticationHandler>(
                    BearerAuthenticationHandler.SchemeName, options => { });
            builder.Services.AddAuthorization();

            // Build app
            var app = builder.Build();

            // Middleware
            app.AddMiddlewares();


            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));



            //CORS
            app.AddCors();

            // Auth
            app.UseAuthentication();
            app.UseAuthorization();

            // Controllers
            app.MapControllers();

            app.Run();
        }
    }
}
