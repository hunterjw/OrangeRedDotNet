using Newtonsoft.Json;
using System.Collections.Generic;

namespace OrangeRedDotNet.Models.LinksAndComments
{
    public class SubmitResponseContent
    {
        [JsonProperty("errors")]
        public List<List<string>> Errors { get; set; }

        [JsonProperty("data")]
        public SubmitResponseData Data { get; set; }
    }
}
