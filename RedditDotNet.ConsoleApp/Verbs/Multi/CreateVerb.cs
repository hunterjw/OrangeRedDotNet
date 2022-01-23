﻿using CommandLine;
using RedditDotNet.Extensions;
using RedditDotNet.Models.Multis;

namespace RedditDotNet.ConsoleApp.Verbs.Multi
{
    /// <summary>
    /// Verb to create a MultiReddit
    /// </summary>
    [Verb("multi-create", HelpText = "Create a MultiReddit")]
    internal class CreateVerb : VerbBase
    {
        /// <summary>
        /// Expand the Subreddits in the result
        /// </summary>
        [Option(HelpText = "Expand the Subreddits in the result")]
        public bool? ExpandSubreddits { get; set; }

        /// <summary>
        /// New MultiReddit path
        /// </summary>
        [Option(Required = true, HelpText = "New MultiReddit path")]
        public string Path { get; set; }

        /// <summary>
        /// Json object of the new MultiReddit, see https://www.reddit.com/dev/api/#POST_api_multi_{multipath}
        /// </summary>
        [Option(Required = true, HelpText = "Json object of the new MultiReddit, see https://www.reddit.com/dev/api/#POST_api_multi_{multipath}")]
        public string Model { get; set; }

        /// <inheritdoc/>
        public override string Run(Reddit reddit)
        {
            return reddit.Multis.CreateMulti(Path, Model.FromJson<MultiRedditUpdate>(), ExpandSubreddits).Result.ToJson();
        }
    }
}