using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RedditDotNet.Models;
using RedditDotNet.Models.Comments;
using RedditDotNet.Models.Links;
using RedditDotNet.Models.Listings;

namespace RedditDotNet.Json
{
    /// <summary>
    /// Json converter for LinkWithComments
    /// </summary>
    public class LinkWithCommentsConverter : JsonConverter
    {
        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(LinkWithComments);
        }

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var toReturn = new LinkWithComments();
            var token = JToken.Load(reader);
            if (token is JArray array)
            {
                if (array.Count > 0)
                {
                    toReturn.Links = array[0].ToObject<Thing<Listing<Thing<Link>>>>();
                }
                if (array.Count > 1)
                {
                    toReturn.Comments = array[1].ToObject<Thing<Listing<Thing<CommentBase>>>>();
                }
            }
            return toReturn;
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is LinkWithComments linkWithComments)
            {
                var array = new JArray
                {
                    JObject.FromObject(linkWithComments.Links),
                    JObject.FromObject(linkWithComments.Comments)
                };
                array.WriteTo(writer);
            }
            else
            {
                throw new NotSupportedException($"Cannot convert {value.GetType().Name} to json using {nameof(LinkWithCommentsConverter)}");
            }
        }
    }
}
