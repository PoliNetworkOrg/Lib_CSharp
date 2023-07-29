#region

using PoliNetwork.Core.Utils.LoggerNS;
using Telegram.Bot.Types;

#endregion

namespace PoliNetwork.Telegram.Objects.Bot;

/// <summary>
///     Telegram Bot Test class used in PoliNetwork projects
/// </summary>
public class TestBot : ITelegramBotWrapper
{
    private readonly Logger _logger;

    public TestBot()
    {
        _logger = new Logger();
    }

    /// <summary>
    ///     Start receiving and handling updates (dummy)
    /// </summary>
    public void Start(CancellationToken cancellationToken)
    {
    }

    /// <summary>
    ///     Send text message (dummy)
    /// </summary>
    /// <param name="chatId">chatId</param>
    /// <param name="text">text</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns></returns>
    public Message SendTextMessage(long chatId, string text, CancellationToken cancellationToken)
    {
        var message = new Message
        {
            From = new User(),
            Text = text,
            MessageId = 1
        };
        return message;
    }

    /// <summary>
    ///     Get logger
    /// </summary>
    /// <returns>logger</returns>
    public Logger GetLogger()
    {
        return _logger;
    }

    public void BanUser(long chatId, long userId, DateTime? untilDate)
    {
    }
}