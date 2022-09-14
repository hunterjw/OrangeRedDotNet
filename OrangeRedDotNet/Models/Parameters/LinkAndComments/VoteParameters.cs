using System.Collections.Generic;

namespace OrangeRedDotNet.Models.Parameters.LinkAndComments
{
    /// <summary>
    /// Vote parameters
    /// </summary>
    public class VoteParameters : QueryParametersBase
    {
        /// <summary>
        /// fullname of a thing
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// vote direction. one of (1, 0, -1)
        /// </summary>
        public int Dir { get; set; }
        /// <summary>
        /// an integer greater than 1
        /// </summary>
        public int Rank { get; set; }

        /// <inheritdoc/>
        public override IDictionary<string, string> ToQueryParameters()
        {
            return new Dictionary<string, string>()
            {
                { "id", Id },
                { "dir", $"{Dir}" },
                { "rank", $"{Rank}" }
            }; ;
        }
    }
}
