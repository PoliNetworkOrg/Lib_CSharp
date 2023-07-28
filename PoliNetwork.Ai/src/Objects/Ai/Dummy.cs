using PoliNetwork.Ai.Objects.Chat;

namespace PoliNetwork.Ai.Objects.Ai;

public class Dummy : IGenericAi
{
    public string GetAnswer(string query, IConversation? conversation = null)
    {
        return conversation != null ? conversation.GetAnswer(query) : $"Dummy {query}";
    }

    public IConversation CreateConversation()
    {
        return new DummyConversation();
    }
}