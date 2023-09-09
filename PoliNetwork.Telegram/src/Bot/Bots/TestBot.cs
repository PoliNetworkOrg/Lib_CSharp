using PoliNetwork.Telegram.Bot.Functionality;
using PoliNetwork.Telegram.Options;
using PoliNetwork.Telegram.Properties;
using PoliNetwork.Telegram.Utility.Logger;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PoliNetwork.Telegram.Bot.Bots;

public class TestBot : AbstractTelegramBot, IEcho
{
    public TestBot(AbstractTelegramBotOptions options, HttpClient? httpClient = null, AbstractLogger? logger = null, User? user = null) : base(options, httpClient, logger, user)
    {
    }

    public TestBot() : base(null, null, null, null)
    {
        ;
    }

    protected override Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    protected override Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public void Echo(IMessage message, CancellationToken cancellationToken)
    {
        return;
    }
}