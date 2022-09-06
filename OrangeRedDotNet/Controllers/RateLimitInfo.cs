using System;
using System.Linq;
using System.Net.Http;

namespace OrangeRedDotNet.Controllers
{
    /// <summary>
    /// Rate limit information
    /// </summary>
    public class RateLimitInfo
    {
        /// <summary>
        /// Get a double value from a response message headers
        /// </summary>
        /// <param name="response">Response message</param>
        /// <param name="name">Header name</param>
        /// <returns>Double (nullable)</returns>
        private static double? GetHeaderValue(HttpResponseMessage response, string name)
        {
            if (response.Headers.Contains(name))
            {
                var values = response.Headers.GetValues(name);
                if (double.TryParse(values.FirstOrDefault(), out double parsed))
                {
                    return parsed;
                }
            }
            return default;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="response">Response message</param>
        public RateLimitInfo(HttpResponseMessage response)
        {
            Used = GetHeaderValue(response, "X-Ratelimit-Used");
            Remaining = GetHeaderValue(response, "X-Ratelimit-Remaining");
            Reset = GetHeaderValue(response, "X-Ratelimit-Reset");

            ReceivedDateTime = response.Headers.Date;
            if (ReceivedDateTime.HasValue && Reset.HasValue)
            {
                ResetDateTime = ReceivedDateTime.Value.AddSeconds(Reset.Value);
            }
        }

        /// <summary>
        /// How many requests have been used
        /// </summary>
        public double? Used { get; set; }
        /// <summary>
        /// How many requests are remaining
        /// </summary>
        public double? Remaining { get; set; }
        /// <summary>
        /// Seconds to the rate limit reset
        /// </summary>
        public double? Reset { get; set; }
        /// <summary>
        /// When this rate limit info was received
        /// </summary>
        public DateTimeOffset? ReceivedDateTime { get; set; }
        /// <summary>
        /// When the rate limit will reset
        /// </summary>
        public DateTimeOffset? ResetDateTime { get; set; }

        /// <summary>
        /// Check if this rate limit info is newer that <paramref name="rateLimitInfo"/>
        /// </summary>
        /// <param name="rateLimitInfo">Info to compare to</param>
        /// <returns>bool value</returns>
        public bool NewerThan(RateLimitInfo rateLimitInfo)
        {
            return (ReceivedDateTime ?? DateTimeOffset.MaxValue) >
                (rateLimitInfo?.ReceivedDateTime ?? DateTimeOffset.MinValue);
        }

        /// <summary>
        /// If there are available requests
        /// </summary>
        /// <returns>bool value</returns>
        public bool HasAvailableRequests()
        {
            return !Remaining.HasValue || Remaining.Value > 0;
        }
    }
}
