#region

using Telegram.Bot.Types;

#endregion

namespace PoliNetwork.Telegram.Objects.Updates;

public class MessageFromBot : IMessage
{
    public MessageFromBot(Message? updateMessage)
    {
        Text = updateMessage?.Text;
        Chat = new ChatFromBot(updateMessage?.Chat);
    }

    public string? Text { get; set; }
    public IChat Chat { get; set; }
}