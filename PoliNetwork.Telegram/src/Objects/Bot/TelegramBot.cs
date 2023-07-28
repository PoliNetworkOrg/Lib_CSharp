#region

using PoliNetwork.Core.Utils.LoggerNS;
using PoliNetwork.Telegram.Objects.Configuration;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

#endregion

namespace PoliNetwork.Telegram.Objects.Bot;

/// <summary>
///     Telegram Bot class used in PoliNetwork projects
/// </summary>
public class TelegramBot : ITelegramBotWrapper
{
    private readonly Logger _logger; //logger
    private readonly TelegramBotClient _telegramBotClient; //telegram bot client
    private readonly UpdateMethod _updateMethod;
    private readonly User _user; //user object representing the bot

    /// <summary>
    ///     Constructor. Generate the bot by token
    /// </summary>
    /// <param name="config">TelegramConfig object containing Database Config and </param>
    /// <param name="updateMethod">updateMethod</param>
    /// <param name="logConfig">configuration for logger</param>
    public TelegramBot(TelegramConfig config, UpdateMethod updateMethod, LogConfig? logConfig = null)
    {
        _updateMethod = updateMethod;
        _telegramBotClient =
            new TelegramBotClient(new TelegramBotClientOptions(config.Token, config.BaseUrl,
                config.UseTestEnvironment));
        _logger = new Logger(logConfig);
        _user = _telegramBotClient.GetMeAsync().Result;
        _logger.Info($"Generated bot. {GetUserString()}");
    }

    /// <summary>
    ///     Start receiving and handling updates
    /// </summary>
    /// <param name="cancellationToken">cancellationToken</param>
    public void Start(CancellationToken cancellationToken)
    {
        // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
        };

        _telegramBotClient.StartReceiving(
            HandleUpdateAsync,
            HandlePollingErrorAsync,
            receiverOptions,
            cancellationToken);

        _logger.Info($"Starting receiving messages. {GetUserString()}");
    }

    /// <summary>
    ///     Send a text message
    /// </summary>
    /// <param name="chatId">chatId to send to</param>
    /// <param name="text">text</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns></returns>
    public async Task<Message?> SendTextMessageAsync(long chatId, string text, CancellationToken cancellationToken)
    {
        return await _telegramBotClient.SendTextMessageAsync(chatId, text, cancellationToken: cancellationToken);
    }

    /// <summary>
    ///     Get logger
    /// </summary>
    /// <returns>logger</returns>
    public Logger GetLogger()
    {
        return _logger;
    }

    private Task HandleUpdateAsync(ITelegramBotClient aTelegramBotClient, Update bUpdate,
        CancellationToken cancellationToken)
    {
        _updateMethod.Run(bUpdate, cancellationToken);
        return Task.CompletedTask;
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