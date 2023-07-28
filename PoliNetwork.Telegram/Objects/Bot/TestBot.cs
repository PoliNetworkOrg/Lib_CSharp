using PoliNetwork.Core.Utils.LoggerNS;
using Telegram.Bot;
using Telegram.Bot.Types;

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
    /// <param name="handleUpdateAsync"></param>
    public void Start(Func<ITelegramBotClient, Update, CancellationToken, Task> handleUpdateAsync)
    {
    }

    public Task<Message?> SendTextMessageAsync(long chatId, string text, CancellationToken cancellationToken)
    {
        var message = new Message
        {
            From = new User(),
            Text = text,
            MessageId = 1
        };
        return Task.FromResult(message);
    }

    public Logger GetLogger()
    {
        return _logger;
    }
}