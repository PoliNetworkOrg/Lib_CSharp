using PoliNetwork.Telegram.Logger;
using PoliNetwork.Telegram.Objects.Bot.Functionality;
using PoliNetwork.Telegram.Objects.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PoliNetwork.Telegram.Objects.Bot;

/// <summary>
///     Telegram Bot Test class used in PoliNetwork projects
/// </summary>
public class TestBot : AbstractTelegramBot, IEcho
{
    public TestBot(AbstractTelegramConfiguration options, HttpClient? httpClient = null, AbstractLogger? logger = null, User? user = null) : base(options, httpClient, logger, user)
    { }

    public static void Start(Func<ITelegramBotClient, Update, CancellationToken, Task> handleUpdateAsync) { }

    public static Task<Message?> SendTextMessageAsync(long chatId, string text, CancellationToken cancellationToken)
    {
        var message = new Message
        {
            From = new User(),
            Text = text,
            MessageId = 1
        };
        return Task.FromResult((Message?)message);
    }

    protected override Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Echo(Message message, CancellationToken cancellationToken)
    {
        if (message.Text is not { } messageText) return;
        long? chatId = message.Chat.Id;

        if (chatId == null) return;

        var sentMessage = this.SendTextMessageAsync(chatId, $"You just typed: {messageText}", cancellationToken: cancellationToken).Result;
        Logger?.Info($"Message: '{messageText}'\nChat: {chatId}\nId: {sentMessage?.MessageId}");
    }

    protected override Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        Configuration.UpdateMethod?.Run(update, cancellationToken);
        return Task.CompletedTask;
    }
}