using BingChat;
using PoliNetwork.Ai.Objects.Chat;

namespace PoliNetwork.Ai.Objects.Ai;


public abstract class Bing : IGenericAi
{
    private readonly BingChatClient _client;

    protected Bing(BingChatTone bingChatTone)
    {
        _client = new BingChatClient(new BingChatClientOptions
        {
            Tone = bingChatTone
        });
    }


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