using PoliNetwork.Core.Utils.LoggerNS;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PoliNetwork.Telegram.Objects.Bot;

public interface ITelegramBotWrapper
{
    public void Start(CancellationToken cancellationToken);
    Task<Message?> SendTextMessageAsync(long chatId, string text, CancellationToken cancellationToken);
    Logger GetLogger();
}