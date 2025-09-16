using Home_test_V1.Models.Interfaces;

namespace Home_test_V1.Models
{
    /// <summary>
    /// Represents application settings such as application name and port.
    /// </summary>
    public class AppSettings : IAppSettings
    {
        /// <summary>
        /// Gets or sets the name of the application.
        /// </summary>
        public string AppName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the port number used by the application.
        /// </summary>
        public int Port { get; set; }
    }
}
