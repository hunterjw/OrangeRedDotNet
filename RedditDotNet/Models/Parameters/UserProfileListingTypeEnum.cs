using System.ComponentModel;

namespace RedditDotNet.Models.Parameters
{
    public enum UserProfileListingType
    {
        [Description("overview")] Overview,
        [Description("submitted")] Submitted,
        [Description("comments")] Comments,
        [Description("upvoted")] Upvoted,
        [Description("downvoted")] Downvoted,
        [Description("hidden")] Hidden,
        [Description("saved")] Saved,
        [Description("gilded")] Gilded
    }
}
