using PoliNetwork.Ai.Objects.Chat;

namespace PoliNetwork.Ai.Objects.Ai;

public interface IGenericAi
{
    string GetAnswer(string query, IConversation? conversation = null);
    IConversation CreateConversation();
}