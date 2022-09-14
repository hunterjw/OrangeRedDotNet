using System.ComponentModel;

namespace OrangeRedDotNet.Models.Parameters.Listings
{
    public enum FrontPageListingType
    {
        [Description("best")] Best,
        [Description("hot")] Hot,
        [Description("new")] New,
        [Description("rising")] Rising,
        [Description("controversial")] Controversial,
        [Description("top")] Top
    }
}
