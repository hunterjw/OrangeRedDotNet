using Newtonsoft.Json;
using OrangeRedDotNet.Authentication;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OrangeRedDotNet.ConsoleApp.Verbs
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
        private static Task<TokenResponse> Load()
        {
            string filePath = GetCacheFilePath();
            if (File.Exists(filePath))
            {
                return Task.FromResult(
                    JsonConvert.DeserializeObject<TokenResponse>(
                        File.ReadAllText(filePath)));
            }
            return Task.FromResult<TokenResponse>(null);
        }

        /// <summary>
        /// Save a TokenResponse
        /// </summary>
        /// <param name="value">Value to save</param>
        private static Task Save(TokenResponse value)
        {
            string filePath = GetCacheFilePath();
            string content = JsonConvert.SerializeObject(value);
            File.WriteAllText(filePath, content);
            return Task.CompletedTask;
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
                    AppSettings.PasswordAuthenticationOptions,
                    Load,
                    Save),
                nameof(ApplicationOnlyAuthentication) => new ApplicationOnlyAuthentication(
                    AppSettings.ApplicationOnlyAuthenticationOptions,
                    Load,
                    Save),
                _ => null,
            };
        }

        /// <summary>
        /// Run this verb
        /// </summary>
        /// <returns>Output for the standard output stream</returns>
        public async Task<string> Run()
        {
            IRedditAuthentication auth = GetAuthentication();
            var reddit = new Reddit(auth, new RedditUserAgent
            {
                Name = AppSettings.GetApplicationName(),
                Version = AppSettings.GetApplicationVersion()
            });
            string result = await Run(reddit);
            return result;
        }

        /// <summary>
        /// Run this verb
        /// </summary>
        /// <param name="reddit">Reddit instance to use to run the verb</param>
        /// <returns>Output for the standard output stream</returns>
        public abstract Task<string> Run(Reddit reddit);
    }
}
