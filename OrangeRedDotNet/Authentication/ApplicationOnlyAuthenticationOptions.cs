namespace OrangeRedDotNet.Authentication
{
    /// <summary>
    /// Options for ApplicationOnlyAuthentication
    /// </summary>
    public class ApplicationOnlyAuthenticationOptions
    {
        /// <summary>
        /// Grant type
        /// </summary>
        public ApplicationOnlyGrantType GrantType { get; set; }
        /// <summary>
        /// Set to true to not have the client ID tracked by Reddit
        /// </summary>
        public bool DoNotTrack { get; set; }
        /// <summary>
        /// Unique Client ID (20 to 30 characters long)
        /// </summary>
        public string DeviceId { get; set; }
        /// <summary>
        /// Reddit app client ID
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// Reddit app client secret
        /// </summary>
        public string ClientSecret { get; set; }
    }
}
