using PoliNetwork.Telegram.Objects.Bot;
using Telegram.Bot.Types;

namespace PoliNetwork.Telegram.Utils;

public static class Echo
{
    /// <summary>
    /// Method to echo back whatever message we received
    /// </summary>
    /// <param name="message">the message we need to echo to</param>
    /// <param name="telegramBotClient">bot</param>
    /// <param name="cancellationToken">cancellationToken</param>
    public static async Task EchoMethod(Message message, ITelegramBotWrapper telegramBotClient,
        CancellationToken cancellationToken)
    {
        // Only process text messages
        if (message.Text is not { } messageText)
            return;

        var chatId = message.Chat.Id;

        // Echo received message text
        var sentMessage = await telegramBotClient.SendTextMessageAsync(
            chatId,
            "You said:\n" + messageText,
            cancellationToken);

        telegramBotClient.GetLogger().Info(
            $"Received a '{messageText}' message in chat {chatId}. Sent {sentMessage?.MessageId} as id of the reply");
    }
}