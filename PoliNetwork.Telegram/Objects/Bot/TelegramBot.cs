using PoliNetwork.Core.Utils.LoggerNS;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PoliNetwork.Telegram.Objects.Bot;

/// <summary>
///     Telegram Bot class used in PoliNetwork projects
/// </summary>
public class TelegramBot : ITelegramBotWrapper
{
    private readonly Logger _logger; //logger
    private readonly TelegramBotClient? _telegramBotClient; //telegram bot client
    private readonly User _user; //user object representing the bot

    /// <summary>
    ///     Constructor. Generate the bot by token
    /// </summary>
    /// <param name="token">token for the bot</param>
    public TelegramBot(string token)
    {
        _telegramBotClient = new TelegramBotClient(token);
        _logger = new Logger();
        _user = _telegramBotClient.GetMeAsync().Result;
        _logger.Info($"Generated bot. {GetUserString()}");
    }

    /// <summary>
    ///     Start receiving and handling updates
    /// </summary>
    /// <param name="handleUpdateAsync">Method to handle updates</param>
    public void Start(Func<ITelegramBotClient, Update, CancellationToken, Task> handleUpdateAsync)
    {
        // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
        };

        _telegramBotClient?.StartReceiving(
            handleUpdateAsync,
            HandlePollingErrorAsync,
            receiverOptions
        );

        _logger.Info($"Starting receiving messages. {GetUserString()}");
    }

    public async Task<Message?> SendTextMessageAsync(long chatId, string text, CancellationToken cancellationToken)
    {
        if (_telegramBotClient == null)
            return null;

        return await _telegramBotClient.SendTextMessageAsync(chatId, text, cancellationToken: cancellationToken);
    }

    public Logger GetLogger()
    {
        return _logger;
    }

    /// <summary>
    ///     Get this bot user string info
    /// </summary>
    /// <returns></returns>
    private string GetUserString()
    {
        return $"Username: {_user.Username}. Id: {_user.Id}";
    }

    /// <summary>
    ///     What will happen in case of telegram errors
    /// </summary>
    /// <param name="botClient">botClient</param>
    /// <param name="exception">exception</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns></returns>
    /// <exception cref="Exception">exception</exception>
    private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}\n{_user.Username} {_user.Id}",
            _ => exception.ToString()
        };

        _logger.Error(errorMessage);
        throw exception;
    }

    /// <summary>
    ///     Get bot telegram id
    /// </summary>
    /// <returns>Bot telegram id</returns>
    public long? GetId()
    {
        return _user.Id;
    }
}