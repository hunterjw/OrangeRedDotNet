using Microsoft.AspNetCore.Components;
using OrangeRedDotNet.BlazorWebApp.Services;
using OrangeRedDotNet.Models.Listings;
using OrangeRedDotNet.Models.Parameters.Listings;
using OrangeRedDotNet.Models.Parameters.Subreddits;
using OrangeRedDotNet.Models.Subreddits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OrangeRedDotNet.BlazorWebApp.Shared.Subreddits
{
    /// <summary>
    /// Text edit with suggestions for a subreddit name
    /// </summary>
    public partial class SubredditSelect
    {
        /// <summary>
        /// Get the icon URL for a partial subreddit
        /// </summary>
        /// <param name="subreddit">Partial subreddit</param>
        /// <returns>Icon URL</returns>
        protected static string GetIconUrl(PartialSubreddit subreddit)
        {
            if (!string.IsNullOrWhiteSpace(subreddit.IconImg))
            {
                return HttpUtility.HtmlDecode(subreddit.IconImg);
            }
            return "icon-512.png";
        }

        /// <summary>
        /// Reddit service
        /// </summary>
        [Inject]
        public RedditService RedditService { get; set; }

        /// <summary>
        /// Exlude subscribed users or not
        /// </summary>
        [Parameter]
        public bool ExcludeSubscribedUsers { get; set; }
        /// <summary>
        /// Subreddit name
        /// </summary>
        [Parameter]
        public string Subreddit
        {
            get => _subreddit;
            set
            {
                if (_subreddit == value)
                {
                    return;
                }
                _subreddit = value;
                SubredditChanged.InvokeAsync(value);
            }
        }
        /// <summary>
        /// Event callback for when the subreddit changed
        /// </summary>
        [Parameter]
        public EventCallback<string> SubredditChanged { get; set; }
        /// <summary>
        /// Event callback for when the text has lost focus
        /// </summary>
        [Parameter]
        public EventCallback OnTextBlur { get; set; }
        /// <summary>
        /// Event callback for when a suggestion is selected
        /// </summary>
        [Parameter]
        public EventCallback OnSuggestionSelected { get; set; }


        /// <summary>
        /// Internal subreddit value
        /// </summary>
        private string _subreddit;

        /// <summary>
        /// My subscriptions
        /// </summary>
        protected List<PartialSubreddit> MySubscriptions { get; set; }
        /// <summary>
        /// Hashset of the names of my subscriptions
        /// </summary>
        protected HashSet<string> MySubscriptionNames { get; set; }
        /// <summary>
        /// My profile
        /// </summary>
        protected PartialSubreddit MyProfile { get; set; }
        /// <summary>
        /// If the suggest edit is disabled or not
        /// </summary>
        protected bool Disabled { get; set; } = true;

        /// <inheritdoc/>
        protected override async Task OnInitializedAsync()
        {
            Reddit client = RedditService.GetClient();
            List<SubredditData> mySubreddits = new List<SubredditData>();
            Listing<Subreddit> lastResponse = null;
            ListingParameters parameters = null;
            do
            {
                if (parameters == null)
                {
                    parameters = new ListingParameters { Limit = 100 };
                }
                else
                {
                    ListingParameters newParameters = parameters.Copy();
                    newParameters.After = newParameters.Before = null;
                    newParameters.After = lastResponse?.Data?.After;
                    if (string.IsNullOrWhiteSpace(parameters.After) && string.IsNullOrWhiteSpace(parameters.Before))
                    {
                        newParameters.Count = newParameters.Limit;
                    }
                    else if (!string.IsNullOrWhiteSpace(parameters.After))
                    {
                        newParameters.Count += newParameters.Limit;
                    }
                    parameters = newParameters;
                }
                lastResponse = await client.Subreddits.GetMine(MySubredditsType.Subscriber, parameters);
                mySubreddits.AddRange(lastResponse?.Data?.Children.Select(_ => _.Data));

            } while (lastResponse?.Data?.Count == 100);

            List<PartialSubreddit> myPartialSubreddits = mySubreddits
                .Where(_ => !ExcludeSubscribedUsers || !_.DisplayName.StartsWith("u_"))
                .Select(_ => _.ToPartial())
                .ToList();

            if (RedditService.Identity.Subreddit != null)
            {
                MyProfile = RedditService.Identity.Subreddit.ToPartial();
            }

            MySubscriptions = myPartialSubreddits;
            MySubscriptionNames = new HashSet<string>(MySubscriptions.Select(_ => _.Name));

            Disabled = false;
        }

        /// <summary>
        /// Get suggestions for a line of text
        /// </summary>
        /// <param name="text">Text to suggestions for</param>
        /// <returns>Enumerable of suggestions</returns>
        protected async Task<IEnumerable<PartialSubreddit>> GetSuggestions(string text)
        {
            List<PartialSubreddit> partialSubreddits = new List<PartialSubreddit>();
            if (MyProfile?.Name.Contains(text, StringComparison.OrdinalIgnoreCase) ?? false)
            {
                partialSubreddits.Add(MyProfile);
            }
            partialSubreddits.AddRange(MySubscriptions
                .Where(_ => _.Name.Contains(text, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(_ => _.SubscriberCount));
            SearchSubredditsResponse searchResponse = await RedditService.GetClient().Subreddits.SearchSubreddits(new()
            {
                Query = text
            });
            partialSubreddits.AddRange(searchResponse.Subreddits.Where(_ => !MySubscriptionNames.Contains(_.Name)));
            return partialSubreddits;
        }
    }
}
