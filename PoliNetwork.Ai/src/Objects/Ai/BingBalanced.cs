#region

using BingChat;

#endregion

namespace PoliNetwork.Ai.Objects.Ai;

public class BingBalanced : Bing
{
    public BingBalanced() : base(BingChatTone.Balanced)
    {
    }
}