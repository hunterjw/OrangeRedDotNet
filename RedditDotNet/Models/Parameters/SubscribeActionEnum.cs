using System.ComponentModel;

namespace RedditDotNet.Models.Parameters
{
    public enum SubscribeAction
    {
        [Description("sub")] Subscribe,
        [Description("unsub")] Unsubscribe
    }
}
