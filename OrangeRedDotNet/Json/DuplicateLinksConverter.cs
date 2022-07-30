using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrangeRedDotNet.Models.Links;
using OrangeRedDotNet.Models.Listings;
using System;

namespace OrangeRedDotNet.Json
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
                    toReturn.Originals = array[0].ToObject<Listing<Link>>();
                }
                if (array.Count > 1)
                {
                    toReturn.Duplicates = array[1].ToObject<Listing<Link>>();
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
