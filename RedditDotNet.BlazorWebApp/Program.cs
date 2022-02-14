using Blazored.LocalStorage;
using Blazored.Modal;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedditDotNet.Authentication;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RedditDotNet.BlazorWebApp
{
    public class Program
    {
        /// <summary>
        /// Password auth options
        /// </summary>
        public static PasswordAuthenticationOptions PasswordAuthenticationOptions { get; } = new();

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Configuration.GetSection(nameof(PasswordAuthenticationOptions)).Bind(PasswordAuthenticationOptions);

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped(sp =>
            {
                var localStorage = sp.GetService<ISyncLocalStorageService>();
                return new Reddit(string.Empty,
                    new CachedPasswordAuthentication(
                        PasswordAuthenticationOptions,
                        () => Load(localStorage),
                        (TokenResponse value) => Save(localStorage, value)));
            });
            builder.Services.AddScoped(sp =>
            {
                var reddit = sp.GetService<Reddit>();
                return new IdentityService(reddit);
            });
            builder.Services.AddBlazoredModal();

            await builder.Build().RunAsync();
        }

        /// <summary>
        /// Load cached auth from local storage
        /// </summary>
        /// <param name="localStorageService">Local storage service</param>
        /// <returns>Cached TokenResponse</returns>
        protected static TokenResponse Load(ISyncLocalStorageService localStorageService)
        {
            if (localStorageService.ContainKey("auth"))
            {
                return localStorageService.GetItem<TokenResponse>("auth");
            }
            return null;
        }

        /// <summary>
        /// Save a TokenResponse to local storage
        /// </summary>
        /// <param name="localStorageService">Local storage service</param>
        /// <param name="value">Value to save</param>
        protected static void Save(ISyncLocalStorageService localStorageService, TokenResponse value)
        {
            localStorageService.SetItem("auth", value);
        }
    }
}
