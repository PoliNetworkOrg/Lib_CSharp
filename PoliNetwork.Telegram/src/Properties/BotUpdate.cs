namespace PoliNetwork.Telegram.Properties;
public class BotUpdate : global::Telegram.Bot.Types.Update, IUpdate
{
    public BotUpdate(global::Telegram.Bot.Types.Update update) : base()
    {
        Message = update.Message;
    }

    IMessage IUpdate.Message => new BotMessage(Message);
}