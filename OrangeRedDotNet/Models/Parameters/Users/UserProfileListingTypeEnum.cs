using System.ComponentModel;

namespace OrangeRedDotNet.Models.Parameters.Users
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
