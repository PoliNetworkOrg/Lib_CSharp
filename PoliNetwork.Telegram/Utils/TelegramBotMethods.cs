using Telegram.Bot;
using Telegram.Bot.Types;

namespace PoliNetwork.Telegram.Utils;

public static class TelegramBotMethods
{
    public static async Task<Message> SendTextMessageAsync(ITelegramBotClient telegramBotClient, long l, string s, CancellationToken cancellationToken1)
    {
        return await telegramBotClient.SendTextMessageAsync(
            chatId: l,
            text: "You said:\n" + s,
            cancellationToken: cancellationToken1);
    }

}