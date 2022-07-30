namespace OrangeRedDotNet.Authentication
{
    /// <summary>
    /// Grant types for ApplicationOnlyAuthentication
    /// </summary>
    public enum ApplicationOnlyGrantType
    {
        /// <summary>
        /// Installed client (can't keep a secret)
        /// </summary>
        InstalledCLient,
        /// <summary>
        /// Hosted client (can keep a secret)
        /// </summary>
        ClientCredentials,
    }
}
