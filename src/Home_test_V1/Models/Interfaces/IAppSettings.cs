namespace Home_test_V1.Models.Interfaces
{
    /// <summary>
    /// Represents application settings configuration.
    /// </summary>
    public interface IAppSettings
    {
        /// <summary>
        /// Gets or sets the name of the application.
        /// </summary>
        string AppName { get; set; }

        /// <summary>
        /// Gets or sets the port number the application uses.
        /// </summary>
        int Port { get; set; }
    }
}
