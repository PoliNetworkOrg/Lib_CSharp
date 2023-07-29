#region

using BingChat;

#endregion

namespace PoliNetwork.Ai.Objects.Chat;

public class BingConversation : IConversation
{
    private readonly BingChatConversation _bingChatConversation;

    public BingConversation(BingChatConversation bingChatConversation)
    {
        _bingChatConversation = bingChatConversation;
    }

    public string GetAnswer(string query)
    {
        return _bingChatConversation.AskAsync(query).Result;
    }
}