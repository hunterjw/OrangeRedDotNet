using Newtonsoft.Json;

namespace RedditDotNet.Extensions
{
    /// <summary>
    /// Extensions for all objects
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Serialize an object to json
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <param name="formatting">Formatting for the json string</param>
        /// <returns>Json string</returns>
        public static string ToJson(this object obj, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(obj, formatting);
        }

        /// <summary>
        /// Deserialize a json string to <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Type to deserialize to</typeparam>
        /// <param name="json">Json string</param>
        /// <returns><typeparamref name="T"/> object</returns>
        public static T FromJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
