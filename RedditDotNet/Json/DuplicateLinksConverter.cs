using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RedditDotNet.Models;
using RedditDotNet.Models.Links;
using RedditDotNet.Models.Listings;
using System;

namespace RedditDotNet.Json
{
    /// <summary>
    /// Json converter for DuplicateLinks
    /// </summary>
    public class DuplicateLinksConverter : JsonConverter
    {
        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DuplicateLinks);
        }

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var toReturn = new DuplicateLinks();
            var token = JToken.Load(reader);
            if (token is JArray array)
            {
                if (array.Count > 0)
                {
                    toReturn.Originals = array[0].ToObject<Thing<Listing<Thing<Link>>>>();
                }
                if (array.Count > 1)
                {
                    toReturn.Duplicates = array[1].ToObject<Thing<Listing<Thing<Link>>>>();
                }
            }
            return toReturn;
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is DuplicateLinks duplicateLinks)
            {
                var array = new JArray
                {
                    JObject.FromObject(duplicateLinks.Originals),
                    JObject.FromObject(duplicateLinks.Duplicates)
                };
                array.WriteTo(writer);
            }
            else
            {
                throw new NotSupportedException($"Cannot convert {value.GetType().Name} to json using {nameof(DuplicateLinksConverter)}");
            }
        }
    }
}
