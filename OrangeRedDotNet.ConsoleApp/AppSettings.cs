using Microsoft.Extensions.Configuration;
using OrangeRedDotNet.Authentication;
using System;
using System.Reflection;

namespace OrangeRedDotNet.ConsoleApp
{
	/// <summary>
	/// Settings for the console application
	/// </summary>
	class AppSettings
	{
		/// <summary>
		/// What authentication provider to use
		/// </summary>
		public static string Authentication { get; }

		/// <summary>
		/// Password authentication options
		/// </summary>
		public static PasswordAuthenticationOptions PasswordAuthenticationOptions { get; } = new();

		/// <summary>
		/// Application only authenticaiton options
		/// </summary>
		public static ApplicationOnlyAuthenticationOptions ApplicationOnlyAuthenticationOptions { get; } = new();

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

			Authentication = configRoot.GetValue<string>(nameof(Authentication));
			configRoot.GetSection(nameof(PasswordAuthenticationOptions))
				.Bind(PasswordAuthenticationOptions);
			configRoot.GetSection(nameof(ApplicationOnlyAuthenticationOptions))
				.Bind(ApplicationOnlyAuthenticationOptions);
		}

		/// <summary>
		/// Get the application name
		/// </summary>
		/// <returns>Application name</returns>
		public static string GetApplicationName()
        {
			return Assembly.GetEntryAssembly().GetName().Name;
		}

		/// <summary>
		/// Get the application version
		/// </summary>
		/// <returns>Version string</returns>
		public static string GetApplicationVersion()
        {
			return Assembly.GetEntryAssembly().GetName().Version.ToString(3);
        }
	}
}
