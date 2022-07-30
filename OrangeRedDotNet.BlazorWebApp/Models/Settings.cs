namespace OrangeRedDotNet.BlazorWebApp.Models
{
    /// <summary>
    /// App settings
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Storage key for settings
        /// </summary>
        public static readonly string LocalStorageKey = "Settings";

        /// <summary>
        /// Default app settings
        /// </summary>
        public static Settings Default
        {
            get
            {
                return new Settings
                {
                    DarkMode = false
                };
            }
        }

        /// <summary>
        /// To use darkmode or not
        /// </summary>
        public bool DarkMode { get; set; }
    }
}
