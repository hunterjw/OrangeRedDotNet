﻿using CommandLine;
using System.Threading.Tasks;

namespace RedditDotNet.ConsoleApp.Verbs.LinksAndComments
{
    /// <summary>
    /// Hide a link.
    /// </summary>
    [Verb("linksandcomments-hide", HelpText = "Hide a link.")]
    internal class HideVerb : VerbBase
    {
        /// <summary>
        /// Fullname of a link
        /// </summary>
        [Option(Required = true, HelpText = "Fullname of a link")]
        public string Id { get; set; }

        /// <inheritdoc/>
        public override async Task<string> Run(Reddit reddit)
        {
            await reddit.LinksAndComments.Hide(Id);
            return string.Empty;
        }
    }
}
