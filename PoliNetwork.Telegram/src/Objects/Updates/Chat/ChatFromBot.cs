namespace PoliNetwork.Telegram.Objects.Updates.Chat;

public class ChatFromBot : IChat
{
    public ChatFromBot(global::Telegram.Bot.Types.Chat? updateMessageChat)
    {
        Id = updateMessageChat?.Id;
    }

    public long? Id { get; set; }
}