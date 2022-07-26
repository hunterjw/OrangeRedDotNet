using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RedditDotNet.Models.Account;
using RedditDotNet.Models.Links;
using RedditDotNet.Models.Listings;
using RedditDotNet.Models.Search;
using RedditDotNet.Models.Subreddits;
using System;

namespace RedditDotNet.Json
{
    /// <summary>
    /// Json converter for SearchResults object
    /// </summary>
    public class SearchResultsConverter : JsonConverter
    {
        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(SearchResults);
        }

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            SearchResults toReturn = new();
            JToken token = JToken.Load(reader);
            if (token is JArray array)
            {
                // Multiple types returned
                foreach (JToken item in array)
                {
                    JToken kindToken = item.SelectToken("data.children[0].kind");
                    if (kindToken != null)
                    {
                        switch (kindToken.ToObject<string>())
                        {
                            case "t2":
                            case "t5":
                                JToken children = item.SelectToken("data.children");
                                foreach (JToken child in children.Children())
                                {
                                    switch (child.SelectToken("kind").ToObject<string>())
                                    {

                                        case "t2":
                                            if (toReturn.Users == null)
                                            {
                                                toReturn.Users = item.ToObject<Listing<Account>>();
                                                toReturn.Users.Data.Children = new();
                                            }
                                            toReturn.Users.Data.Children.Add(child.ToObject<Account>());
                                            break;
                                        case "t5":
                                            if (toReturn.Subreddits == null)
                                            {
                                                toReturn.Subreddits = item.ToObject<Listing<Subreddit>>();
                                                toReturn.Subreddits.Data.Children = new();
                                            }
                                            toReturn.Subreddits.Data.Children.Add(child.ToObject<Subreddit>());
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                break;
                            case "t3":
                                toReturn.Links = item.ToObject<Listing<Link>>();
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
            {
                // Single type returned
                var kindToken = token.SelectToken("data.children[0].kind");
                if (kindToken != null)
                {
                    switch (kindToken.ToObject<string>())
                    {
                        case "t2":
                            toReturn.Users = token.ToObject<Listing<Account>>();
                            break;
                        case "t3":
                            toReturn.Links = token.ToObject<Listing<Link>>();
                            break;
                        case "t5":
                            toReturn.Subreddits = token.ToObject<Listing<Subreddit>>();
                            break;
                    }
                }
            }
            return toReturn;
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is SearchResults searchResults)
            {
                // TODO Should I somehow combine subreddits and users back together?
                // TODO Should I return an object when only one result type is populated?
                var array = new JArray();

                if (searchResults?.Subreddits != null)
                {
                    array.Add(JObject.FromObject(searchResults.Subreddits));
                }
                if (searchResults?.Users != null)
                {
                    array.Add(JObject.FromObject(searchResults.Users));
                }
                if (searchResults?.Links != null)
                {
                    array.Add(JObject.FromObject(searchResults.Links));
                }
                array.WriteTo(writer);
            }
            else
            {
                throw new NotSupportedException($"Cannot convert {value.GetType().Name} to json using {nameof(SearchResultsConverter)}");
            }
        }
    }
}
