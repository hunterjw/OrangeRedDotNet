using Blazored.LocalStorage;
using RedditDotNet.Authentication;
using RedditDotNet.Extensions;
using RedditDotNet.Models.Account;
using System;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp.Services
{
    /// <summary>
    /// Service for interacting with Reddit
    /// </summary>
    public class RedditService
    {
        /// <summary>
        /// App only auth options
        /// </summary>
        private readonly ApplicationOnlyAuthenticationOptions _appOnlyAuthOpts;
        /// <summary>
        /// OAuth auth options
        /// </summary>
        private readonly OAuthAuthenticationOptions _oauthAuthOpts;
        /// <summary>
        /// Local storage service
        /// </summary>
        private readonly ISyncLocalStorageService _localStorageService;

        /// <summary>
        /// Current identity
        /// </summary>
        private AccountData _identity;
        /// <summary>
        /// Current Reddit authentication
        /// </summary>
        private IRedditAuthentication _redditAuthentication = default;
        /// <summary>
        /// Current Reddit client
        /// </summary>
        private Reddit _reddit = default;
        /// <summary>
        /// If the user is logged in or not
        /// </summary>
        private bool _loggedIn = false;

        /// <summary>
        /// Event handler for when the login operation finishes
        /// </summary>
        public event EventHandler<object> LoginFinished;
        /// <summary>
        /// Event handler for when the logout operation finishes
        /// </summary>
        public event EventHandler<object> LogoutFinished;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appOnlyAuthOpts">App only auth options</param>
        /// <param name="oauthAuthOpts">OAuth auth options</param>
        /// <param name="localStorageService">Local storage service</param>
        public RedditService(ApplicationOnlyAuthenticationOptions appOnlyAuthOpts,
            OAuthAuthenticationOptions oauthAuthOpts,
            ISyncLocalStorageService localStorageService)
        {
            _appOnlyAuthOpts = appOnlyAuthOpts;
            _oauthAuthOpts = oauthAuthOpts;
            _localStorageService = localStorageService;

            string loggedInString = LocalStorageLoad("loggedIn");
            if (!string.IsNullOrWhiteSpace(loggedInString))
            {
                _loggedIn = bool.Parse(loggedInString);
            }

            GetClient();
        }

        /// <summary>
        /// Load a string from local storage
        /// </summary>
        /// <param name="key">Storage key</param>
        /// <returns>Stored content, null if key does not exist</returns>
        private string LocalStorageLoad(string key)
        {
            if (_localStorageService.ContainKey(key))
            {
                return _localStorageService.GetItemAsString(key);
            }
            return null;
        }

        /// <summary>
        /// Save a string to local storage
        /// </summary>
        /// <param name="key">Key to save to</param>
        /// <param name="value">Value to save</param>
        private void LocalStorageSave(string key, string value)
        {
            _localStorageService.SetItemAsString(key, value);
        }

        /// <summary>
        /// Clear the contents of a key in local storage
        /// </summary>
        /// <param name="key">Key to clear</param>
        private void LocalStorageClear(string key)
        {
            if (_localStorageService.ContainKey(key))
            {
                _localStorageService.RemoveItem(key);
            }
        }

        /// <summary>
        /// Get the Reddit auth object
        /// </summary>
        /// <returns>IRedditAuthentication instance</returns>
        private IRedditAuthentication GetAuthentication()
        {
            if (_redditAuthentication == default)
            {
                if (_loggedIn)
                {
                    _redditAuthentication = new OAuthAuthentication(
                        _oauthAuthOpts,
                        () => LocalStorageLoad("auth")?.FromJson<TokenResponse>(),
                        (value) => LocalStorageSave("auth", value.ToJson()));
                }
                else
                {
                    _redditAuthentication = new ApplicationOnlyAuthentication(_appOnlyAuthOpts,
                        () => LocalStorageLoad("appAuth")?.FromJson<TokenResponse>(),
                        (value) => LocalStorageSave("appAuth", value.ToJson()));
                }
            }
            return _redditAuthentication;
        }

        /// <summary>
        /// Get a Reddit client
        /// </summary>
        /// <returns>Reddit client</returns>
        public Reddit GetClient()
        {
            if (_reddit == default)
            {
                _reddit = new(GetAuthentication());
            }
            return _reddit;
        }

        /// <summary>
        /// Load the current users identity
        /// </summary>
        /// <param name="forceReload">To force reload from Reddit or not</param>
        /// <returns>Identity information</returns>
        public async Task<AccountData> LoadIdentity(bool forceReload = false)
        {
            if (!_loggedIn)
            {
                return null;
            }
            if (_identity == null || forceReload)
            {
                _identity = await GetClient().Account.GetIdentity();
            }
            return _identity;
        }

        /// <summary>
        /// Current user Identity
        /// </summary>
        public AccountData Identity
        {
            get => _identity;
        }

        /// <summary>
        /// If the user is logged in or not
        /// </summary>
        public bool LoggedIn
        {
            get => _loggedIn;
        }

        /// <summary>
        /// Login to Reddit
        /// </summary>
        /// <returns>Awaitable task</returns>
        public async Task Login()
        {
            if (!_loggedIn)
            {
                _loggedIn = true;
                _reddit = default;
                _redditAuthentication = default;
                _identity = await GetClient().Account.GetIdentity();
                LocalStorageSave("loggedIn", _loggedIn.ToString());
                LoginFinished?.Invoke(this, new object());
            }
        }

        /// <summary>
        /// Logout of Reddit
        /// </summary>
        /// <returns>Awaitable task</returns>
        public async Task Logout()
        {
            if (_loggedIn)
            {
                await GetAuthentication().RevokeToken();
                _loggedIn = false;
                _reddit = default;
                _redditAuthentication = default;
                _identity = default;
                LocalStorageClear("auth");
                LocalStorageSave("loggedIn", _loggedIn.ToString());
                LogoutFinished?.Invoke(this, new object());
            }
        }

        /// <summary>
        /// Get an authorization URL to direct the user to (for logging in to Reddit)
        /// </summary>
        /// <returns>Authorization URL string</returns>
        public async Task<string> GetAuthorizationUrl()
        {
            string state = Guid.NewGuid().ToString();
            LocalStorageSave("authState", state);
            return await new OAuthAuthentication(_oauthAuthOpts).GetAuthorizationUrl(state);
        }

        /// <summary>
        /// Parse the results from the authorization redirect and update the login state
        /// </summary>
        /// <param name="code">One time code from Reddit</param>
        /// <param name="state">Application state</param>
        /// <param name="error">Any errors that occurred</param>
        /// <returns>Awaitable task</returns>
        public async Task ParseRedirectUrl(string code, string state, string error)
        {
            string storedState = LocalStorageLoad("authState");
            OAuthAuthentication auth = new(_oauthAuthOpts,
                save: (value) => LocalStorageSave("auth", value.ToJson()));
            await auth.ParseRedirectUrl(code, state, storedState, error);
        }
    }
}
