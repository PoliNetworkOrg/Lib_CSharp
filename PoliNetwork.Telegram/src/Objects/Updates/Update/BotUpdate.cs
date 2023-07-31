using PoliNetwork.Telegram.Objects.Updates.Chat;
using PoliNetwork.Telegram.Objects.Updates.Message;
using PoliNetwork.Telegram.Objects.Updates.User;

namespace PoliNetwork.Telegram.Objects.Updates.Update;

public class BotUpdate : global::Telegram.Bot.Types.Update, IUpdate
{
    private readonly IChat _chat;
    private readonly IUser _user;

    public BotUpdate(IChat chat, IUser user) : base()
    {
        _chat = chat;
        _user = user;
    }

    IMessage IUpdate.Message => new BotMessage(_chat, _user);
}