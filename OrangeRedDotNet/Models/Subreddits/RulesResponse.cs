using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
