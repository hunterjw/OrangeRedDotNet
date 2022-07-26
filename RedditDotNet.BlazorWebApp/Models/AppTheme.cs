using Blazorise;

namespace RedditDotNet.BlazorWebApp.Models
{
    /// <summary>
    /// App theme settings
    /// </summary>
    public class AppTheme
    {
        /// <summary>
        /// If dark mode is set or not
        /// </summary>
        public bool DarkMode { get; set; }
        /// <summary>
        /// Default text color
        /// </summary>
        public TextColor TextColor { get; set; }
        /// <summary>
        /// Default background color
        /// </summary>
        public Background Background { get; set; }
        /// <summary>
        /// Default theme contrast
        /// </summary>
        public ThemeContrast ThemeContrast { get; set; }
        /// <summary>
        /// Spacing for things that are not related
        /// </summary>
        public IFluentSpacing SpacingSeparate { get; set; }
        /// <summary>
        /// Spacing for things that are related
        /// </summary>
        public IFluentSpacing SpacingRelated { get; set; }
        /// <summary>
        /// spacing for things that are the same
        /// </summary>
        public IFluentSpacing SpacingSame { get; set; }
        /// <summary>
        /// Border around cards and other components
        /// </summary>
        public IFluentBorder Border { get; set; }
        /// <summary>
        /// Default button color
        /// </summary>
        public Color DefaultButtonColor { get; set; }
    }
}
