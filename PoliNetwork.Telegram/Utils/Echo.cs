using PoliNetwork.Telegram.Objects.Bot;
using Telegram.Bot.Types;

namespace PoliNetwork.Telegram.Utils;

public static class Echo
{
    public static async Task EchoMethod(Message message, ITelegramBotWrapper telegramBotClient, CancellationToken cancellationToken)
    {
        // Only process text messages
        if (message.Text is not { } messageText)
            return;

        var chatId = message.Chat.Id;

        // Echo received message text
        var sentMessage =  await telegramBotClient.SendTextMessageAsync(
            chatId: chatId,
            text: "You said:\n" + messageText,
            cancellationToken: cancellationToken);
        
        telegramBotClient.GetLogger().Info(
            $"Received a '{messageText}' message in chat {chatId}. Sent {sentMessage?.MessageId} as id of the reply");
    }
}