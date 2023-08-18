using PoliNetwork.Telegram.Bot.Functionality;
using PoliNetwork.Telegram.Logger;
using PoliNetwork.Telegram.Options;
using PoliNetwork.Telegram.Properties;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace PoliNetwork.Telegram.Bot.Bots
{
    public class ModerationBot : AbstractTelegramBot, IEcho
    {
        public ModerationBot(AbstractTelegramBotOptions options, HttpClient? httpClient = null, AbstractLogger? logger = null, User? user = null)
        : base(options, httpClient, logger, user) { }

        public void Echo(IMessage message, CancellationToken cancellationToken)
        {
            if (message.Text is not { } messageText) return;
            long? chatId = message.Chat.Id;

            var sentMessage = this.SendTextMessageAsync(chatId, $"You just typed: {messageText}", cancellationToken: cancellationToken).Result;
            Logger?.Info($"Message: '{messageText}'\nChat: {chatId}\nId: {sentMessage?.MessageId}");
        }

        protected override Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                  => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}\n{User?.Username} {User?.Id}",
                _ => exception.ToString()
            };

            Logger?.Error(errorMessage);
            throw exception;
        }

        protected override Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Options?.UpdateMethod?.Run(update, cancellationToken);
            return Task.CompletedTask;
        }
    }
}