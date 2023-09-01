using PoliNetwork.Telegram.Properties;

public class BotMessage : global::Telegram.Bot.Types.Message, IMessage
{
    public BotMessage(global::Telegram.Bot.Types.Message message)
    {
        base.Chat = message.Chat;
        base.From = message.From;
    }

    public new IChat Chat => new BotChat(base.Chat);
    public new IUser From => new BotUser(base.From);
}