using Home_test_V1.Models.Interfaces;

namespace Home_test_V1.Models
{

    /// <summary>
    /// Represents the settings required for JWT authentication.
    /// </summary>
    public class JwtSettings : IJwtSettings
    {
        /// <summary>
        /// Gets or sets the secret key used to sign the JWT token.
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the issuer of the JWT token.
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the expiry time (in minutes) for the JWT token.
        /// </summary>
        public int ExpiryMinutes { get; set; }
    }
}
