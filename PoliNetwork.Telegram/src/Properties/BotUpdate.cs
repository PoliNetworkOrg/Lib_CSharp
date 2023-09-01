using PoliNetwork.Telegram.Options;

namespace PoliNetwork.Telegram.Properties;
public class BotUpdate : global::Telegram.Bot.Types.Update, IUpdate
{
    public BotUpdate(global::Telegram.Bot.Types.Update update)
    {
        Message = (BotMessage)update.Message!;
    }

    public new BotMessage? Message
    {
        get => Message != null ? new BotMessage(Message) : null;
        set => throw new NotImplementedException();
    }
}