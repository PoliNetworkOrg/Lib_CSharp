#region

using PoliNetwork.Telegram.Objects.Updates.Chat;

#endregion

namespace PoliNetwork.Telegram.Objects.Updates.Message;

public class MessageFromBot : IMessage
{
    public MessageFromBot(global::Telegram.Bot.Types.Message? updateMessage)
    {
        Text = updateMessage?.Text;
        Chat = new ChatFromBot(updateMessage?.Chat);
    }

    public string? Text { get; set; }
    public IChat Chat { get; set; }
}