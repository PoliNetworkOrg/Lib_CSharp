using PoliNetwork.Telegram.Bot.Bots;
using PoliNetwork.Telegram.Options;

namespace PoliNetwork.Telegram.Properties;
public class BotUpdate : global::Telegram.Bot.Types.Update, IUpdate
{
    public BotUpdate(global::Telegram.Bot.Types.Update update) : base()
    {
        Message = update.Message;
    }

    BotMessage IUpdate.Message => Message != null ? new BotMessage(Message) : null;


}