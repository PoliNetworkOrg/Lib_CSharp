#region

using PoliNetwork.Core.Utils.LoggerNS;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

#endregion

namespace PoliNetwork.Telegram.Objects.Bot;

public interface ITelegramBotWrapper
{
    public void Start(CancellationToken cancellationToken);
    Message SendTextMessage(long chatId, string text, CancellationToken cancellationToken, IReplyMarkup? iReplyMarkup = null, int? replyToMessageId = null);
    Logger GetLogger();
    void BanUser(long chatId, long userId, DateTime? untilDate);
    void SendLocation(Update update, Location location);
}