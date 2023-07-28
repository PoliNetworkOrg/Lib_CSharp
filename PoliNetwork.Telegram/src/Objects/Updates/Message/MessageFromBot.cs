#region

using PoliNetwork.Telegram.Objects.Updates.Chat;
using PoliNetwork.Telegram.Objects.Updates.User;

#endregion

namespace PoliNetwork.Telegram.Objects.Updates.Message;

public class MessageFromBot : IMessage
{
    public MessageFromBot(global::Telegram.Bot.Types.Message? updateMessage)
    {
        Text = updateMessage?.Text;
        From = new UserFromBot(updateMessage?.From);
        Chat = new ChatFromBot(updateMessage?.Chat);
    }

    public string? Text { get; set; }
    public IChat Chat { get; set; }
    public IUser From { get; set; }
}