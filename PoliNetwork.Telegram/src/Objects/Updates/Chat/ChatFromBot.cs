#region

using PoliNetwork.Telegram.Objects.Updates.Type;

#endregion

namespace PoliNetwork.Telegram.Objects.Updates.Chat;

public class ChatFromBot : IChat
{
    public ChatFromBot(global::Telegram.Bot.Types.Chat? updateMessageChat)
    {
        Id = updateMessageChat?.Id;
        Username = updateMessageChat?.Username;
        Type = GetChatType(updateMessageChat?.Type);
    }


    public long? Id { get; set; }
    public string? Username { get; set; }
    public ChatType? Type { get; set; }


    public bool ValidUsername()
    {
        return !string.IsNullOrEmpty(Username) && Username.Length > 4;
    }

    private static ChatType? GetChatType(global::Telegram.Bot.Types.Enums.ChatType? type)
    {
        return type switch
        {
            global::Telegram.Bot.Types.Enums.ChatType.Private => ChatType.Private,
            global::Telegram.Bot.Types.Enums.ChatType.Group => ChatType.Group,
            global::Telegram.Bot.Types.Enums.ChatType.Channel => ChatType.Channel,
            global::Telegram.Bot.Types.Enums.ChatType.Supergroup => ChatType.Supergroup,
            global::Telegram.Bot.Types.Enums.ChatType.Sender => ChatType.Sender,
            _ => null
        };
    }
}