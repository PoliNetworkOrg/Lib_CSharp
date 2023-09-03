#region

using PoliNetwork.Telegram.Objects.Updates.Type;

#endregion

namespace PoliNetwork.Telegram.Objects.Updates.Chat;

public class BotChat : global::Telegram.Bot.Types.Chat, IChat
{
    /* Alternative to switch statement */
    /* Will you extend the Types of the chats? 'global::Telegram.Bot.Types.Enums.ChatType' is the same as 'Telegram.Objects.Updates.Type' */
    private static readonly Dictionary<global::Telegram.Bot.Types.Enums.ChatType, ChatType> _chatTypes = new(){
        { global::Telegram.Bot.Types.Enums.ChatType.Private, ChatType.Private },
        { global::Telegram.Bot.Types.Enums.ChatType.Group, ChatType.Group },
        { global::Telegram.Bot.Types.Enums.ChatType.Channel, ChatType.Channel },
        { global::Telegram.Bot.Types.Enums.ChatType.Supergroup, ChatType.Supergroup },
        { global::Telegram.Bot.Types.Enums.ChatType.Sender, ChatType.Sender }
    };
    public new ChatType? Type => GetChatType(base.Type);

    private static ChatType? GetChatType(global::Telegram.Bot.Types.Enums.ChatType type) => _chatTypes[type];

    public bool ValidUsername()
    {
        const int MIN_LENGTH_USERNAME = 4;
        return !string.IsNullOrEmpty(Username) && Username.Length > MIN_LENGTH_USERNAME;
    }
}