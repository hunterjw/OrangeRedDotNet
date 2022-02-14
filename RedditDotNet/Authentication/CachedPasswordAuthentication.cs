using System;
using System.Threading.Tasks;

namespace RedditDotNet.Authentication
{
    /// <summary>
    /// Password Authentication with the retrieved token saved for use between app runs
    /// </summary>
    public class CachedPasswordAuthentication : CachedAuthentication
    {
        /// <summary>
        /// Input options
        /// </summary>
        private readonly PasswordAuthenticationOptions _options;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Password auth options</param>
        public CachedPasswordAuthentication(PasswordAuthenticationOptions options) 
            : this(options, null, null) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Password auth options</param>
        /// <param name="load">Function to load cached auth</param>
        /// <param name="save">Action to save auth</param>
        public CachedPasswordAuthentication(PasswordAuthenticationOptions options, 
            Func<TokenResponse> load, Action<TokenResponse> save) 
            : base(load, save)
        {
            _options = options;
        }

        /// <inheritdoc/>
        protected override Task<TokenResponse> GetFreshToken()
        {
            return PasswordAuthentication.GetFreshToken(_options);
        }
    }
}
