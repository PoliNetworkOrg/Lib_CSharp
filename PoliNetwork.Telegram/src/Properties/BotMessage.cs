using PoliNetwork.Telegram.Bot.Bots;
using Telegram.Bot.Types;


namespace PoliNetwork.Telegram.Properties;

using PoliNetwork.Telegram.Properties;


public class BotMessage : global::Telegram.Bot.Types.Message
{
    public BotMessage(Message? message)
    {
        base.Chat = message.Chat;
        base.From = message.From;
    }



    public new IUser? From
    {
        get
        {
            var from = base.From;
            return from != null ? new BotUser(from) : null;
        }
    }



}