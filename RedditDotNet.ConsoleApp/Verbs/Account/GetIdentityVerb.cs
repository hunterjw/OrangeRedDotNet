﻿using CommandLine;
using RedditDotNet.Extensions;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp.Verbs.Account
{
	/// <summary>
	/// Get the identity of the current user
	/// </summary>
	[Verb("account-get-identity", HelpText = "Get the identity of the current user")]
	internal class GetIdentityVerb : VerbBase
	{
		/// <inheritdoc/>
		public override async Task<string> Run(Reddit reddit)
		{
			return (await reddit.Account.GetIdentity()).ToJson();
		}
	}
}
