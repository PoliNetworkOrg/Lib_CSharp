#region

using PoliNetwork.Core.Utils.LoggerNS;
using Telegram.Bot.Types;

#endregion

namespace PoliNetwork.Telegram.Objects.Bot;

public interface ITelegramBotWrapper
{
    public void Start(CancellationToken cancellationToken);
    Message SendTextMessage(long chatId, string text, CancellationToken cancellationToken);
    Logger GetLogger();
    void BanUser(long chatId, long userId, DateTime? untilDate);
}