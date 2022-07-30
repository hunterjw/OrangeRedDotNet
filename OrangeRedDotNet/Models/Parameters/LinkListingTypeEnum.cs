﻿using System.ComponentModel;

namespace OrangeRedDotNet.Models.Parameters
{
    public enum LinkListingType
    {
        [Description("hot")] Hot,
        [Description("new")] New,
        [Description("rising")] Rising,
        [Description("controversial")] Controversial,
        [Description("top")] Top
    }
}