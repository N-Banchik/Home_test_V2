using Home_test_V1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

/// <summary>
/// Authentication handler for validating Bearer (JWT) tokens, checking only the issuer and expiration time.
/// </summary>
public class BearerAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly TokenValidationParameters _tokenValidationParameters;

    /// <summary>
    /// Scheme name for the Bearer authentication handler.
    /// </summary>
    public const string SchemeName = "Bearer";

    /// <summary>
    /// Initializes a new instance of the <see cref="BearerAuthenticationHandler"/> class.
    /// </summary>
    /// <param name="options">The authentication scheme options.</param>
    /// <param name="logger">The logger factory for logging authentication events.</param>
    /// <param name="encoder">The URL encoder.</param>
    /// <param name="clock">The system clock for validating token expiration.</param>
    /// <param name="configuration">The configuration for JWT settings.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="configuration"/> is null.</exception>
    public BearerAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IConfiguration configuration)
        : base(options, logger, encoder, clock)
    {
        _tokenHandler = new JwtSecurityTokenHandler();
        var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
        var key = Encoding.ASCII.GetBytes(jwtSettings.Key);
        _tokenValidationParameters = new TokenValidationParameters
        {
           ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidIssuer = jwtSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    }

    /// <summary>
    /// Validates the Authorization header and authenticates the JWT token based on issuer and expiration time.
    /// </summary>
    /// <returns>An <see cref="AuthenticateResult"/> indicating success or failure.</returns>
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.Fail("Missing Authorization Header");
        }

        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            if (authHeader.Scheme != "Bearer" || string.IsNullOrEmpty(authHeader.Parameter))
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            // Validate the JWT token
            var principal = _tokenHandler.ValidateToken(authHeader.Parameter, _tokenValidationParameters, out var validatedToken);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
        catch (SecurityTokenExpiredException ex)
        {
            return AuthenticateResult.Fail($"Token expired: {ex.Message}");
        }
        catch (SecurityTokenInvalidIssuerException ex)
        {
            return AuthenticateResult.Fail($"Invalid issuer: {ex.Message}");
        }
        catch (SecurityTokenException ex)
        {
            return AuthenticateResult.Fail($"Invalid JWT: {ex.Message}");
        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail($"Authentication failed: {ex.Message}");
        }
    }
}