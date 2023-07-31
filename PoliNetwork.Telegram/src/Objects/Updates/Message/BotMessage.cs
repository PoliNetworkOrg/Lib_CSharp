#region

using PoliNetwork.Telegram.Objects.Updates.Chat;
using PoliNetwork.Telegram.Objects.Updates.User;

#endregion

namespace PoliNetwork.Telegram.Objects.Updates.Message;

public class BotMessage : global::Telegram.Bot.Types.Message, IMessage
{
    private readonly IChat _chat;
    private readonly IUser _user;

    public BotMessage(IChat chat, IUser user) : base()
    {
        _chat = chat;
        _user = user;
    }

    IChat IMessage.Chat => _chat;

    IUser IMessage.From => _user;
}