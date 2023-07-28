using BingChat;
using PoliNetwork.Ai.Objects.Chat;

namespace PoliNetwork.Ai.Objects.Ai;

public class Bing : IGenericAi
{
    private readonly BingChatClient _client = new(new BingChatClientOptions
    {
        Tone = BingChatTone.Balanced
    });
    
    public string GetAnswer(string query, IConversation? conversation = null)
    {
        return conversation != null ? conversation.GetAnswer(query) : _client.AskAsync(query).Result;
    }

    public IConversation CreateConversation()
    {
        var bingChatConversation = _client.CreateConversation().Result;
        return new BingConversation(bingChatConversation);
    }
}