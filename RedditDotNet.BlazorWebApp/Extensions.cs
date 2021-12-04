using System;

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
        /// Convert a unix timestamp to a UTC DateTime
        /// </summary>
        /// <param name="timestampUtc">Unix timestamp</param>
        /// <returns>UTC DateTime</returns>
        public static DateTime ToDateTimeUtc(this double timestampUtc)
        {
            return DateTimeOffset.FromUnixTimeSeconds((long)timestampUtc)
                .ToLocalTime().DateTime;
        }
    }
}
