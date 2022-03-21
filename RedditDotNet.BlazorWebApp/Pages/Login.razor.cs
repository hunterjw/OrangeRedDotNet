using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Pages
{
    /// <summary>
    /// Login page
    /// </summary>
    public partial class Login
    {
        /// <summary>
        /// Reddit service
        /// </summary>
        [Inject]
        public RedditService RedditService { get; set; }

        /// <summary>
        /// Navigation manager
        /// </summary>
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        /// <summary>
        /// One time code from Reddit
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery]
        public string Code { get; set; }

        /// <summary>
        /// Application state
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery]
        public string State { get; set; }

        /// <summary>
        /// Any errors encountered
        /// </summary>
        [Parameter]
        [SupplyParameterFromQuery]
        public string Error { get; set; }

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            if (!RedditService.LoggedIn)
            {
                if (string.IsNullOrWhiteSpace(Error))
                {
                    if (!string.IsNullOrEmpty(Code) &&
                        !string.IsNullOrWhiteSpace(State))
                    {
                        await RedditService.ParseRedirectUrl(
                            Code[..Code.IndexOf('#')], 
                            State, Error);
                        await RedditService.Login();
                        NavigationManager.NavigateTo("");
                    }
                    else
                    {
                        var authUrl = await RedditService.GetAuthorizationUrl();
                        NavigationManager.NavigateTo(authUrl);
                    }
                }
            }
            NavigationManager.NavigateTo("");
        }
    }
}
