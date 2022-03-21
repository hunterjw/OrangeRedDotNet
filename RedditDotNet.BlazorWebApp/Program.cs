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
        public static ApplicationOnlyAuthenticationOptions ApplicationOnlyAuthenticationOptions { get; } = new();

        public static OAuthAuthenticationOptions OAuthAuthenticationOptions { get; } = new();

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Configuration
                .GetSection(nameof(ApplicationOnlyAuthenticationOptions))
                .Bind(ApplicationOnlyAuthenticationOptions);
            builder.Configuration
                .GetSection(nameof(OAuthAuthenticationOptions))
                .Bind(OAuthAuthenticationOptions);

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped(sp =>
            {
                var localStorage = sp.GetService<ISyncLocalStorageService>();
                return new RedditService(ApplicationOnlyAuthenticationOptions,
                    OAuthAuthenticationOptions,
                    localStorage);
            });
            builder.Services.AddBlazoredModal();

            await builder.Build().RunAsync();
        }
    }
}
