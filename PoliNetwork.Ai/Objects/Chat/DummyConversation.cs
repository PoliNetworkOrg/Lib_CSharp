namespace PoliNetwork.Ai.Objects.Chat;

public class DummyConversation : IConversation
{
    private int _i = 0; 
    
    public string GetAnswer(string query)
    {
        _i++;
        return $"DummyConversation {_i} {query}";
    }
}