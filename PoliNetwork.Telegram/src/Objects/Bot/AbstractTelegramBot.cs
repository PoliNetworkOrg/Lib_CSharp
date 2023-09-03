using PoliNetwork.Telegram.Logger;
using PoliNetwork.Telegram.Objects.Configuration;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PoliNetwork.Telegram.Objects.Bot
{
    public abstract class AbstractTelegramBot : TelegramBotClient
    {
        public User? User { get; set; }
        public AbstractLogger? Logger { get; set; }
        public AbstractTelegramConfiguration Configuration { get; set; }

        protected AbstractTelegramBot(AbstractTelegramConfiguration options, HttpClient? httpClient = null, AbstractLogger? logger = null, User? user = null)
        : base(options, httpClient)
        {
            Logger = logger;
            User = user ?? this.GetMeAsync().Result;
            Logger?.Info($"CONSTRUCTED BOT: {User.Id} ({User.Username})");
            /* Better to have the GetUsername method inside a User sublcass or do as above, 
            each class sould have one and only one responability: User class or a UserManager class are responsible for the user logic */
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