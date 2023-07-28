#region

using Telegram.Bot.Types;

#endregion

namespace PoliNetwork.Telegram.Objects.Updates;

public class ChatFromBot : IChat
{
    public ChatFromBot(Chat? updateMessageChat)
    {
        Id = updateMessageChat?.Id;
    }

    public long? Id { get; set; }
}