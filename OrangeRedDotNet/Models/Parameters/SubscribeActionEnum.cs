using System.ComponentModel;

namespace OrangeRedDotNet.Models.Parameters
{
    public enum SubscribeAction
    {
        [Description("sub")] Subscribe,
        [Description("unsub")] Unsubscribe
    }
}
