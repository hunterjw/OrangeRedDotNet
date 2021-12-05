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

        public static SortListingParameters BuildSortListingParameters(string after, string before, int? count, int? limit)
        {
            return new SortListingParameters
            {
                After = after,
                Before = before,
                Count = count ?? 0,
                Limit = limit ?? 25
            };
        }

        public static async Task<string> BuildNextPageUrl(string relativeUrl, string after, ListingParameters currentPageParameters)
        {
            var parameters = new ListingParameters
            {
                After = currentPageParameters.After,
                Before = currentPageParameters.Before,
                Count = currentPageParameters.Count,
                Limit = currentPageParameters.Limit
            };
            parameters.After = parameters.Before = null;
            parameters.After = after;
            if (string.IsNullOrWhiteSpace(currentPageParameters.After) && string.IsNullOrWhiteSpace(currentPageParameters.Before))
            {
                parameters.Count = parameters.Limit;
            }
            else if (!string.IsNullOrWhiteSpace(currentPageParameters.After))
            {
                parameters.Count += parameters.Limit;
            }
            return $"{relativeUrl}?{await new FormUrlEncodedContent(parameters.ToQueryParameters()).ReadAsStringAsync()}";
        }

        public static async Task<string> BuildPreviousPageUrl(string relativeUrl, string before, ListingParameters currentPageParameters)
        {
            var parameters = new ListingParameters
            {
                After = currentPageParameters.After,
                Before = currentPageParameters.Before,
                Count = currentPageParameters.Count,
                Limit = currentPageParameters.Limit
            };
            parameters.After = parameters.Before = null;
            parameters.Before = before;
            if (!string.IsNullOrWhiteSpace(currentPageParameters.Before))
            {
                parameters.Count -= parameters.Limit;
            }
            return $"{relativeUrl}?{await new FormUrlEncodedContent(parameters.ToQueryParameters()).ReadAsStringAsync()}";
        }
    }
}
