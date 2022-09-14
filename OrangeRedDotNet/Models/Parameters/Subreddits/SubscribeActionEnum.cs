using System.ComponentModel;

namespace OrangeRedDotNet.Models.Parameters.Subreddits
{
    public enum SubscribeAction
    {
        [Description("sub")] Subscribe,
        [Description("unsub")] Unsubscribe
    }
}
