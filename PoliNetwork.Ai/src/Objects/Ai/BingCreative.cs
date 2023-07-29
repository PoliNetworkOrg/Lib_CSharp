#region

using BingChat;

#endregion

namespace PoliNetwork.Ai.Objects.Ai;

public class BingCreative : Bing
{
    public BingCreative() : base(BingChatTone.Creative)
    {
    }
}