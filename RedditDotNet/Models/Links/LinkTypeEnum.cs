namespace RedditDotNet.Models.Links
{
    /// <summary>
    /// Type of a link
    /// </summary>
    public enum LinkType
    {
        /// <summary>
        /// Unable to determine Link type
        /// </summary>
        Unknown,
        /// <summary>
        /// Link to an external page
        /// </summary>
        Link,
        /// <summary>
        /// Image link
        /// </summary>
        Image,
        /// <summary>
        /// Video link
        /// </summary>
        Video,
        /// <summary>
        /// Image gallery link
        /// </summary>
        Gallery,
        /// <summary>
        /// Poll link
        /// </summary>
        Poll,
        /// <summary>
        /// Crosspost link
        /// </summary>
        Crosspost,
        /// <summary>
        /// Text link (aka self post)
        /// </summary>
        Text,
        /// <summary>
        /// Link with embedded media
        /// </summary>
        EmbeddedMedia
    }
}
