using BingChat;

namespace PoliNetwork.Ai.Utils;

public class Bing : IGenericAi
{
    private readonly BingChatClient _client = new(new BingChatClientOptions
    {
        Tone = BingChatTone.Balanced
    });
    
    public string GetAnswer(string query)
    {
        return _client.AskAsync(query).Result;
    }
}