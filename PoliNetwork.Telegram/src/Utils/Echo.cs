#region

using PoliNetwork.Telegram.Objects.Bot;
using PoliNetwork.Telegram.Objects.Updates.Message;

#endregion

namespace PoliNetwork.Telegram.Utils;

public static class Echo
{
    /// <summary>
    ///     Method to echo back whatever message we received
    /// </summary>
    /// <param name="message">the message we need to echo to</param>
    /// <param name="telegramBotClient">bot</param>
    /// <param name="cancellationToken">cancellationToken</param>
    public static void EchoMethod(IMessage message, ITelegramBotWrapper telegramBotClient,
        CancellationToken cancellationToken)
    {
        // Only process text messages
        var messageText = message.Text;
        if (string.IsNullOrEmpty(messageText))
            return;

        var messageChat = message.Chat;
        var chatId = messageChat.Id;

        // Echo received message text
        if (chatId == null) return;

        var sentMessage = telegramBotClient.SendTextMessage(
            chatId.Value,
            "You said:\n" + messageText,
            cancellationToken);

        telegramBotClient.GetLogger().Info(
            $"Received a '{messageText}' message in chat {chatId}. Sent {sentMessage.MessageId} as id of the reply");
    }
}