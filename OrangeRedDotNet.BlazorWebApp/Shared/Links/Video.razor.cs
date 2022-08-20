using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.Models.Links;
using System.Threading.Tasks;

namespace OrangeRedDotNet.BlazorWebApp.Shared.Links
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
        /// Theme service
        /// </summary>
        [Inject]
        public AppThemeService ThemeService { get; set; }

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
