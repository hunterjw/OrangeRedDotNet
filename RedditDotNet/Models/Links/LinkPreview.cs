using Newtonsoft.Json;
using System.Collections.Generic;

namespace RedditDotNet.Models.Links
{
	/// <summary>
	/// Preview images for a link
	/// </summary>
	public class LinkPreview
	{
		[JsonProperty("images")]
		public List<PreviewImage> Images { get; set; }

		[JsonProperty("enabled")]
		public bool Enabled { get; set; }
	}
}
