using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RedditDotNet.Interfaces;
using RedditDotNet.Models.Comments;
using RedditDotNet.Models.Links;
using System;

namespace RedditDotNet.Json
{
    /// <summary>
    /// Json converter for ILinkOrComment
    /// </summary>
    public class LinkOrCommentConverter : JsonConverter
    {
        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            return typeof(ILinkOrComment).IsAssignableFrom(objectType);
        }

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType == typeof(ILinkOrComment))
            {
                var jobj = JToken.Load(reader) as JObject;
                if (jobj["kind"].Value<string>().Equals("t3"))
                {
                    return jobj.ToObject<Link>();
                }
                else
                {
                    return jobj.ToObject<CommentBase>();
                }
            }
            else
            {
                serializer.ContractResolver.ResolveContract(objectType).Converter = null;
                return serializer.Deserialize(reader, objectType);
            }
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JObject.FromObject(value).WriteTo(writer);
        }
    }
}
