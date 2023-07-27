using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PoliNetwork.Telegram.Objects;

public interface TelegramBotWrapper
{
    public abstract void Start(Func<ITelegramBotClient, Update, CancellationToken, Task> handleUpdateAsync);

}

public class TestBot : TelegramBotWrapper
{
    public void Start(Func<ITelegramBotClient, Update, CancellationToken, Task> handleUpdateAsync)
    {
        return;
    }
}

public class TelegramBot : TelegramBotWrapper
{
    private readonly TelegramBotClient? _telegramBotClient;

    public TelegramBot(string token)
    {
        this._telegramBotClient = new TelegramBotClient(token);
    }


    public void Start(Func<ITelegramBotClient, Update, CancellationToken, Task> handleUpdateAsync)
    {
        // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
        };

        this._telegramBotClient?.StartReceiving(
            updateHandler: handleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: receiverOptions
        );

    }

    private static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(errorMessage);
        throw exception;
    }


}