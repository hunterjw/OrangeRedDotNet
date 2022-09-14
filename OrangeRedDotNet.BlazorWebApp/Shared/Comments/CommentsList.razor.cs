using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.Models.Comments;
using OrangeRedDotNet.Models.Listings;
using OrangeRedDotNet.Models.Parameters.Listings;

namespace OrangeRedDotNet.BlazorWebApp.Shared.Comments
{
    /// <summary>
    /// Comments list component
    /// </summary>
    public partial class CommentsList
    {
        #region Parameters
        /// <summary>
        /// Comments to display
        /// </summary>
        [Parameter]
        public Listing<CommentBase> Comments { get; set; }
        /// <summary>
        /// The original poster for the parent link that these comments belongs to
        /// </summary>
        [Parameter]
        public string OriginalPoster { get; set; }
        /// <summary>
        /// The full name of the link these comments belong to
        /// </summary>
        [Parameter]
        public string LinkFullName { get; set; }
        /// <summary>
        /// The sort of the comments
        /// </summary>
        [Parameter]
        public CommentSort? CommentSort { get; set; }
        #endregion
    }
}
