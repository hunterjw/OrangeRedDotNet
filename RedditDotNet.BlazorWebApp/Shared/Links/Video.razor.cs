using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RedditDotNet.Models.Links;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Shared.Links
{
    /// <summary>
    /// Component for displaying RedditVideo
    /// </summary>
    public partial class Video
    {
        /// <summary>
        /// JS interop
        /// </summary>
        [Inject]
        IJSRuntime JS { get; set; }

        /// <summary>
        /// Reddit video object
        /// </summary>
        [Parameter]
        public RedditVideo RedditVideo { get; set; }
        /// <summary>
        /// Link ID (used for generating a unique video element id)
        /// </summary>
        [Parameter]
        public string LinkId { get; set; }

        /// <inheritdoc/>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JS.InvokeVoidAsync("attachVideo", $"{LinkId}-video");
        }
    }
}
