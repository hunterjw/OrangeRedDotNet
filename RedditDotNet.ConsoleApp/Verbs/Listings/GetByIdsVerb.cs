using CommandLine;
using RedditDotNet.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

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
		public override async Task<string> Run(Reddit reddit)
		{
			return (await reddit.Listings.GetByIds(Ids)).ToJson();
		}
	}
}
