#region

using PoliNetwork.Telegram.Objects.Updates.Chat;
using PoliNetwork.Telegram.Objects.Updates.User;

#endregion

namespace PoliNetwork.Telegram.Objects.Updates.Message;

public class MessageFromBot : IMessage
{
    private readonly global::Telegram.Bot.Types.Message? _message;

    public MessageFromBot(global::Telegram.Bot.Types.Message? updateMessage)
    {
        _message = updateMessage;
    }

    public string? Text => _message?.Text;
    public IChat Chat => new ChatFromBot(_message?.Chat);
    public IUser From => new UserFromBot(_message?.From);
}