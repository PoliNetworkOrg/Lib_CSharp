using System;
using System.Threading;
using System.Threading.Tasks;
using PoliNetwork.Core.Utils.LoggerNS;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PoliNetwork.Telegram.Objects.Bot;

public interface ITelegramBotWrapper
{
    public void Start(Func<ITelegramBotClient, Update, CancellationToken, Task> handleUpdateAsync);
    Task<Message?> SendTextMessageAsync(long chatId, string text, CancellationToken cancellationToken);
    Logger GetLogger();
}