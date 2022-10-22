using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OrangeRedDotNet.Authentication;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

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
        /// Test subreddit display name
        /// </summary>
        protected static string TestSubreddit { get; set;  } = string.Empty;

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
            TestSubreddit = configRoot.GetValue<string>(nameof(TestSubreddit));
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

        /// <summary>
        /// Run a test with a test subreddit, throwing an inconclusive assertion if a test subreddit is not configured
        /// </summary>
        /// <param name="testContent">Test content</param>
        /// <returns>Awaitable task</returns>
        protected static async Task RunWithTestSubreddit(Func<string, Task> testContent)
        {
            if (string.IsNullOrWhiteSpace(TestSubreddit))
            {
                Assert.Inconclusive($"Missing {nameof(TestSubreddit)} in configuration.");
            }
            else
            {
                await testContent(TestSubreddit);
            }
        }
    }
}
