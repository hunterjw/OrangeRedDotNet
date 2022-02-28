using Newtonsoft.Json;
using RedditDotNet.Authentication;
using System;
using System.IO;

namespace RedditDotNet.ConsoleApp.Verbs
{
    /// <summary>
    /// Base class for command line Reddit verbs
    /// </summary>
    internal abstract class VerbBase
    {
        /// <summary>
        /// Get the path to the cache file
        /// </summary>
        /// <returns>File path</returns>
        private static string GetCacheFilePath()
        {
            return Path.Combine(AppContext.BaseDirectory, "authCache");
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
        /// Cleanup the authentication after running the verb
        /// </summary>
        /// <param name="authentication">Authentication to cleanup</param>
        private static void CleanupAuthentication(IRedditAuthentication authentication)
        {
            if (authentication is PasswordAuthentication passwordAuthentication)
            {
                passwordAuthentication.Dispose();
            }
            else if (authentication is ApplicationOnlyAuthentication applicationOnlyAuthentication)
            {
                applicationOnlyAuthentication.Dispose();
            }
        }

        /// <summary>
        /// Get the authentication object to access reddit
        /// </summary>
        /// <returns>Reddit authentication</returns>
        private IRedditAuthentication GetAuthentication()
        {
            return AppSettings.Authentication switch
            {
                nameof(PasswordAuthentication) => new PasswordAuthentication(
                    AppSettings.PasswordAuthenticationOptions),
                nameof(CachedPasswordAuthentication) => new CachedPasswordAuthentication(
                    AppSettings.PasswordAuthenticationOptions,
                    Load,
                    Save),
                nameof(ApplicationOnlyAuthentication) => new ApplicationOnlyAuthentication(
                    AppSettings.ApplicationOnlyAuthenticationOptions),
                _ => null,
            };
        }

        /// <summary>
        /// Run this verb
        /// </summary>
        /// <returns>Output for the standard output stream</returns>
        public string Run()
        {
            IRedditAuthentication auth = GetAuthentication();
            var reddit = new Reddit("C# Test 1.0.0", auth); // TODO need to generate proper user agent string here
            string result = Run(reddit);
            CleanupAuthentication(auth);
            return result;
        }

        /// <summary>
        /// Run this verb
        /// </summary>
        /// <param name="reddit">Reddit instance to use to run the verb</param>
        /// <returns>Output for the standard output stream</returns>
        public abstract string Run(Reddit reddit);
    }
}
