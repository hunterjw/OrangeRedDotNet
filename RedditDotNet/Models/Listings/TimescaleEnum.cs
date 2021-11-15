﻿using System.ComponentModel;

namespace RedditDotNet.Models.Listings
{
	/// <summary>
	/// Timescale enum
	/// </summary>
	public enum Timescale
	{
		[Description("hour")] Hour, 
		[Description("day")] Day, 
		[Description("week")] Week, 
		[Description("month")] Month, 
		[Description("year")] Year, 
		[Description("all")] All
	}
}
