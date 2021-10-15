using Microsoft.Extensions.Configuration;
using RedditDotNet.Authentication;
using System;

namespace RedditDotNet.ConsoleApp
{
	/// <summary>
	/// Settings for the console application
	/// </summary>
	class AppSettings
	{
		/// <summary>
		/// Password authentication options
		/// </summary>
		public static PasswordAuthenticationOptions PasswordAuthenticationOptions { get; } = new();

		/// <summary>
		/// Static constructor
		/// </summary>
		static AppSettings()
		{
			var configRoot = new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
				.AddJsonFile("appsettings.json")
				.AddUserSecrets<AppSettings>()
				.Build();

			configRoot.GetSection(nameof(PasswordAuthenticationOptions)).Bind(PasswordAuthenticationOptions);
		}
	}
}
