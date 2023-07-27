using Telegram.Bot;
using Telegram.Bot.Types;

namespace PoliNetwork.Telegram.Utils;

public static class Echo
{
    public static async Task EchoMethod(Message message1, ITelegramBotClient telegramBotClient, CancellationToken cancellationToken1)
    {
        // Only process text messages
        if (message1.Text is not { } messageText)
            return;

        var chatId = message1.Chat.Id;

        // Echo received message text
        var sentMessage =
            await TelegramBotMethods.SendTextMessageAsync(telegramBotClient, chatId, messageText, cancellationToken1);
        Console.WriteLine(
            $"Received a '{messageText}' message in chat {chatId}. Sent {sentMessage.MessageId} as id of the reply");
    }
}