using RedditDotNet.Exceptions;
using System;
using System.Linq;

namespace RedditDotNet.BlazorWebApp
{
    /// <summary>
    /// Extensions for the Reddit web app
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Convert an int (like a score) to a display friendly string
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>Display string</returns>
        public static string ToDisplayString(this int value)
        {
            if (value > 1000000)
            {
                return $"{Math.Round((double)value / 1000000, 1)}M";
            }
            else if (value > 1000)
            {
                return $"{Math.Round((double)value / 1000, 1)}K";
            }
            return $"{value}";
        }

        /// <summary>
        /// Convert a unix timestamp to a local DateTime
        /// </summary>
        /// <param name="timestampUtc">Unix timestamp</param>
        /// <returns>Local DateTime</returns>
        public static DateTime ToLocalDateTime(this double timestampUtc)
        {
            return DateTimeOffset.FromUnixTimeSeconds((long)timestampUtc)
                .ToLocalTime().DateTime;
        }

        /// <summary>
        /// Format a MultiReddit display name into a subpath
        /// </summary>
        /// <param name="input">Display name</param>
        /// <returns>Path component</returns>
        public static string FormatNewMultiName(this string input) =>
            new(input.ToLower().Select(_ => char.IsWhiteSpace(_) ? '_' : _).ToArray());

        /// <summary>
        /// Make an error message to show to the user
        /// </summary>
        /// <param name="redditApiException">Reddit API exception</param>
        /// <param name="error">Error description</param>
        /// <returns>Error message</returns>
        public static string MakeErrorMessage(this RedditApiException redditApiException, string error)
        {
            string message = string.Empty;
            message += error;
            if (!string.IsNullOrWhiteSpace(error) &&
                !string.IsNullOrWhiteSpace(redditApiException?.ResponseContent?.Explanation))
            {
                message += ": ";
            }
            message += redditApiException?.ResponseContent?.Explanation;
            return message;
        }
    }
}
