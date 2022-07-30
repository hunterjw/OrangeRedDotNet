using Newtonsoft.Json;
using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Subreddits
{
    public class NextStepReason
    {
        [JsonProperty("reasonTextToShow")]
        public string ReasonTextToShow { get; set; }

        [JsonProperty("reasonText")]
        public string ReasonText { get; set; }

        [JsonProperty("nextStepHeader")]
        public string NextStepHeader { get; set; }

        [JsonProperty("nextStepReasons")]
        public List<NextStepReason> NextStepReasons { get; set; }

        [JsonProperty("canWriteNotes")]
        public bool? CanWriteNotes { get; set; }

        [JsonProperty("isAbuseOfReportButton")]
        public bool? IsAbuseOfReportButton { get; set; }

        [JsonProperty("notesInputTitle")]
        public string NotesInputTitle { get; set; }

        [JsonProperty("complaintButtonText")]
        public string ComplaintButtonText { get; set; }

        [JsonProperty("complaintUrl")]
        public string ComplaintUrl { get; set; }

        [JsonProperty("complaintPageTitle")]
        public string ComplaintPageTitle { get; set; }

        [JsonProperty("fileComplaint")]
        public bool? FileComplaint { get; set; }

        [JsonProperty("complaintPrompt")]
        public string ComplaintPrompt { get; set; }

        [JsonProperty("usernamesInputTitle")]
        public string UsernamesInputTitle { get; set; }

        [JsonProperty("canSpecifyUsernames")]
        public bool? CanSpecifyUsernames { get; set; }

        [JsonProperty("requestCrisisSupport")]
        public bool? RequestCrisisSupport { get; set; }

        [JsonProperty("oneUsername")]
        public bool? OneUsername { get; set; }
    }
}
