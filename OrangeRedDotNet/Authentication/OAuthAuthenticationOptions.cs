using System.Collections.Generic;

namespace OrangeRedDotNet.Authentication
{
    /// <summary>
    /// Options for OAuthAuthentication
    /// </summary>
    public class OAuthAuthenticationOptions
    {
        /// <summary>
        /// Client ID
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// Redirect URL
        /// </summary>
        public string RedirectUrl { get; set; }
        /// <summary>
        /// Duration of the returned token
        /// </summary>
        public OAuthDuration Duration { get; set; }
        /// <summary>
        /// Scopes to request
        /// </summary>
        public List<string> Scopes { get; set; } = new();
        /// <summary>
        /// To show the compact authorization page or not
        /// </summary>
        public bool Compact { get; set; } = false;
    }
}
