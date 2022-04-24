using Newtonsoft.Json;
using System.Collections.Generic;

namespace RedditDotNet.Models.Subreddits
{
    public class SiteRulesFlow
    {
        [JsonProperty("reasonTextToShow")]
        public string ReasonTextToShow { get; set; }

        [JsonProperty("reasonText")]
        public string ReasonText { get; set; }

        [JsonProperty("nextStepHeader")]
        public string NextStepHeader { get; set; }

        [JsonProperty("nextStepReasons")]
        public List<NextStepReason> NextStepReasons { get; set; }
    }
}
