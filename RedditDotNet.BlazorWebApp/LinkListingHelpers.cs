using RedditDotNet.Models.Listings;
using System.Net.Http;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp
{
    public static class LinkListingHelpers
    {
        public static ListingParameters BuildListingParameters(string after, string before, int? count, int? limit)
        {
            return new ListingParameters
            {
                After = after,
                Before = before,
                Count = count ?? 0,
                Limit = limit ?? 25
            };
        }

        public static LocationListingParameters BuildLocationListingParameters(string after, string before, int? count, int? limit)
        {
            return new LocationListingParameters
            {
                After = after,
                Before = before,
                Count = count ?? 0,
                Limit = limit ?? 25
            };
        }

        public static async Task<string> BuildNextPageUrl(string relativeUrl, string after, ListingParameters currentPageParameters)
        {
            var parameters = currentPageParameters;
            parameters.After = parameters.Before = null;
            parameters.Count += parameters.Limit;
            parameters.After = after;
            return $"{relativeUrl}?{await new FormUrlEncodedContent(parameters.ToQueryParameters()).ReadAsStringAsync()}";
        }

        public static async Task<string> BuildPreviousPageUrl(string relativeUrl, string before, ListingParameters currentPageParameters)
        {
            var parameters = currentPageParameters;
            parameters.After = parameters.Before = null;
            parameters.Count -= parameters.Limit;
            parameters.Before = before;
            return $"{relativeUrl}?{await new FormUrlEncodedContent(parameters.ToQueryParameters()).ReadAsStringAsync()}";
        }
    }
}
