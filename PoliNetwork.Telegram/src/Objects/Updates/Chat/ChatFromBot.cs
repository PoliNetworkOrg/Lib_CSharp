#region

using PoliNetwork.Telegram.Objects.Updates.Type;

#endregion

namespace PoliNetwork.Telegram.Objects.Updates.Chat;

public class ChatFromBot : IChat
{
    private readonly global::Telegram.Bot.Types.Chat? _chat;

    public ChatFromBot(global::Telegram.Bot.Types.Chat? updateMessageChat)
    {
        _chat = updateMessageChat;
    }

    public long? Id => _chat?.Id;
    public string? Username => _chat?.Username;

    public ChatType? Type => GetChatType(_chat?.Type);


    public bool ValidUsername()
    {
        const int minLengthUsername = 4;
        return !string.IsNullOrEmpty(Username) && Username.Length > minLengthUsername;
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