namespace RedditDotNet.Authentication
{
    /// <summary>
    /// OAuth authentication duration
    /// </summary>
    public enum OAuthDuration
    {
        /// <summary>
        /// Returned token only valid for one hour
        /// </summary>
        Temporary = 0,
        /// <summary>
        /// Returned token can be refreshed with a refresh token
        /// </summary>
        Permanent = 1,
    }
}
