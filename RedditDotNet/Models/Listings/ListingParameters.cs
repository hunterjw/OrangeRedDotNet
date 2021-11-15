using RedditDotNet.Interfaces;
using System;
using System.Collections.Generic;

namespace RedditDotNet.Models.Listings
{
	/// <summary>
	/// Parameters for listings
	/// </summary>
	public class ListingParameters : IQueryParameters
	{
		/// <summary>
		/// Fullname of a thing
		/// </summary>
		public string After { get; set; }
		/// <summary>
		/// Fullname of a thing
		/// </summary>
		public string Before { get; set; }
		/// <summary>
		/// Number of items already retrieved
		/// </summary>
		public int Count { get; set; } = 0;
		/// <summary>
		/// Maximum number of things to return
		/// </summary>
		public int Limit { get; set; } = 25;
		/// <summary>
		/// To show all or not (bypasses preferences that would hide results)
		/// </summary>
		public bool? ShowAll { get; set; }
		/// <summary>
		/// Expand subreddit references into objects
		/// </summary>
		public bool? ExpandSubreddits { get; set; }

		/// <inheritdoc/>
		public IDictionary<string, string> ToQueryParameters()
		{
			var dict = new Dictionary<string, string>
			{
				{ "count", $"{Count}" },
				{ "limit", $"{Limit}" }
			};
			if (!string.IsNullOrWhiteSpace(After))
			{
				dict.Add("after", After);
			}
			if (!string.IsNullOrWhiteSpace(Before))
			{
				dict.Add("before", Before);
			}
			if (ShowAll.HasValue && ShowAll.Value)
			{
				dict.Add("show", "all");
			}
			if (ExpandSubreddits.HasValue && ExpandSubreddits.Value)
			{
				dict.Add("sr_detail", "true");
			}
			return dict;
		}

		/// <inheritdoc/>
		public void Validate()
		{
			List<string> errors = new();

			if (Count < 0)
			{
				errors.Add($"{nameof(Count)} must be a positive integer");
			}
			if (Limit < 0 || Limit > 100)
			{
				errors.Add($"{nameof(Limit)} must be a positive integer between 0 and 100");
			}

			if (errors.Count > 0)
			{
				throw new ArgumentException(string.Join(Environment.NewLine, errors));
			}
		}
	}
}
