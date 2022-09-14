using Newtonsoft.Json;
using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Subreddits
{
    public class RulesResponse
    {
        [JsonProperty("rules")]
        public List<Rule> Rules { get; set; }

        [JsonProperty("site_rules")]
        public List<string> SiteRules { get; set; }

        [JsonProperty("site_rules_flow")]
        public List<SiteRulesFlow> SiteRulesFlow { get; set; }
    }
}
