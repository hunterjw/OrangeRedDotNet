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
		public static PasswordAuthenticationOptions PasswordAuthenticationOptions { get; } = new();

		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

			builder.Configuration.GetSection(nameof(PasswordAuthenticationOptions)).Bind(PasswordAuthenticationOptions);
			builder.Services.AddScoped(sp => new Reddit(string.Empty, new PasswordAuthentication(PasswordAuthenticationOptions)));

			await builder.Build().RunAsync();
		}
	}
}
