using CommandLine;
using System.Collections.Generic;

namespace RedditDotNet.ConsoleApp.Verbs.Listings
{
	/// <summary>
	/// Get Links by ID(s)
	/// </summary>
	[Verb("listings-get-byids", HelpText = "Get Links by ID(s)")]
	class GetByIdsVerb : VerbBase
	{
		/// <summary>
		/// Full names of links to get
		/// </summary>
		[Option(Min = 1, Separator = ',', HelpText = "Full names of links to get")]
		public IEnumerable<string> Ids { get; set; }

		/// <inheritdoc/>
		public override string Run(Reddit reddit)
		{
			return ToJson(reddit.Listings.GetByIds(Ids).Result);
		}
	}
}
