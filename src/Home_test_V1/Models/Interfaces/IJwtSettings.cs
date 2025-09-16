namespace Home_test_V1.Models.Interfaces
{
    /// <summary>
    /// Represents the settings required for JWT authentication.
    /// </summary>
    public interface IJwtSettings
    {
        /// <summary>
        /// Gets or sets the secret key used to sign the JWT.
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// Gets or sets the issuer of the JWT.
        /// </summary>
        string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the expiry time in minutes for the JWT.
        /// </summary>
        int ExpiryMinutes { get; set; }
    }
}
