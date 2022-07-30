using OrangeRedDotNet.Models.Multis;

namespace OrangeRedDotNet.BlazorWebApp.Shared.Multis
{
    /// <summary>
    /// State of the selected MultiReddits
    /// </summary>
    public class MultiRedditState
    {
        /// <summary>
        /// MultiReddit
        /// </summary>
        public MultiReddit MultiReddit { get; set; }
        /// <summary>
        /// Original state
        /// </summary>
        public bool OriginalState { get; set; }
        /// <summary>
        /// Current state
        /// </summary>
        public bool CurrentState { get; set; }
    }
}
