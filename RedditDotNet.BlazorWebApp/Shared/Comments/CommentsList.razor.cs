﻿using Microsoft.AspNetCore.Components;
using RedditDotNet.Models.Comments;
using RedditDotNet.Models.Listings;

namespace RedditDotNet.BlazorWebApp.Shared.Comments
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
        #endregion
    }
}
