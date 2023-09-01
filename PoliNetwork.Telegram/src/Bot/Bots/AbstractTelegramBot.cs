using PoliNetwork.Telegram.Logger;
using PoliNetwork.Telegram.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PoliNetwork.Telegram.Bot.Bots
{
    public abstract class AbstractTelegramBot : TelegramBotClient
    {
        public User? User { get; set; }
        public AbstractLogger? Logger { get; set; }
        public AbstractTelegramBotOptions? Options { get; set; }

        protected AbstractTelegramBot(AbstractTelegramBotOptions? options, HttpClient? httpClient = null, AbstractLogger? logger = null, User? user = null)
        : base(options ?? new TelegramBotOptions(""), httpClient)
        {
            Logger = logger;
            User = user ?? this.GetMeAsync().Result;
            Logger?.Info($"CONSTRUCTED BOT: {User.Id} ({User.Username})");
        }

        public void Start(CancellationToken cancellationToken)
        {
            /* Doesn't block the caller thread. Receiving is done on the ThreadPool */
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>() /* Ignore ChatMember updates */
            };

            this.StartReceiving(HandleUpdateAsync, HandlePollingErrorAsync, receiverOptions, cancellationToken);

            Logger?.Info($"RECEAVING MESSAGES FROM: {User?.Id} ({User?.Username})");
        }

        protected abstract Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken);

        protected abstract Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken); 
    }

    /* Extend the AbstractTelegramBot or implement specific interfaces to add functionality,
    if a bot is not responsible for e.g banning users, it shouldn't have a method to abilitate it:

    e.g
    interface IBanCapable {
        public void BanUser(long chatId, long userId, DateTime? untilDate);
    }

    class TelegramAdminBot : AbstractTelegramBot, IBanCapable { ... } 

    */
}