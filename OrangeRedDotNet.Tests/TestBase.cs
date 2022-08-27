using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OrangeRedDotNet.Authentication;
using System;
using System.IO;
using System.Reflection;

namespace OrangeRedDotNet.Tests
{
    /// <summary>
    /// Base class for tests
    /// </summary>
    public class TestBase
    {
        /// <summary>
        /// Get the path to the cache file
        /// </summary>
        /// <returns>File path</returns>
        private static string GetCacheFilePath()
        {
            return Path.Combine(AppContext.BaseDirectory, $"AuthCache");
        }

        /// <summary>
        /// Load a cached TokenResponse
        /// </summary>
        /// <returns>Cached TokenResponse</returns>
        private static TokenResponse Load()
        {
            string filePath = GetCacheFilePath();
            if (File.Exists(filePath))
            {
                return JsonConvert.DeserializeObject<TokenResponse>(
                    File.ReadAllText(filePath));
            }
            return null;
        }

        /// <summary>
        /// Save a TokenResponse
        /// </summary>
        /// <param name="value">Value to save</param>
        private static void Save(TokenResponse value)
        {
            string filePath = GetCacheFilePath();
            string content = JsonConvert.SerializeObject(value);
            File.WriteAllText(filePath, content);
        }
        /// <summary>
        /// Password authentication options
        /// </summary>
        protected static PasswordAuthenticationOptions PasswordAuthenticationOptions { get; } = new();

        /// <summary>
        /// Constructor
        /// </summary>
        static TestBase()
        {
            var configRoot = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddUserSecrets<TestBase>()
                .Build();

            configRoot.GetSection(nameof(PasswordAuthenticationOptions))
                .Bind(PasswordAuthenticationOptions);
        }

        /// <summary>
        /// Get reddit authentication
        /// </summary>
        /// <returns>Reddit authentication</returns>
        private static IRedditAuthentication GetAuthentication()
        {
            return new PasswordAuthentication(
                    PasswordAuthenticationOptions,
                    Load, Save);
        }

        /// <summary>
        /// Get a reddit client
        /// </summary>
        /// <returns>A reddit client</returns>
        protected static Reddit GetRedditClient()
        {
            return new Reddit(
                GetAuthentication(),
                new RedditUserAgent
                {
                    Name = Assembly.GetEntryAssembly().GetName().Name,
                    Version = Assembly.GetEntryAssembly().GetName().Version.ToString(3)
                });
        }
    }
}
