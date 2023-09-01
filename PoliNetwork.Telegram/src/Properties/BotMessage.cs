<<<<<<< HEAD
namespace PoliNetwork.Telegram.Properties;
=======
using PoliNetwork.Telegram.Properties;
>>>>>>> v3

public class BotMessage : global::Telegram.Bot.Types.Message, IMessage
{
    public BotMessage(global::Telegram.Bot.Types.Message message)
    {
        base.Chat = message.Chat;
        base.From = message.From;
    }

    public new IChat Chat => new BotChat(base.Chat);
<<<<<<< HEAD
    public new IUser? From
    {
        get
        {
            var from = base.From;
            return from != null ? new BotUser(from) : null;
        }
    }
=======
    public new IUser From => new BotUser(base.From);
>>>>>>> v3
}