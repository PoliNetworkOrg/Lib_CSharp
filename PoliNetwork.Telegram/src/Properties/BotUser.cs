namespace PoliNetwork.Telegram.Properties;

public class BotUser : global::Telegram.Bot.Types.User, IUser
{
    public static readonly long MIN_NAME_LENGTH = 2;

    public BotUser() : base() { }

    public BotUser(global::Telegram.Bot.Types.User user) : base() { 
        base.Id = user.Id;
        base.Username = user.Username;
        base.FirstName = user.FirstName;
        base.LastName = user.LastName;
    }

    /* Should handle this in a different way: in case we want to change Validation logic for running Bots */
    public bool? ValidName() => !string.IsNullOrEmpty(FirstName) && FirstName.Length > MIN_NAME_LENGTH;
}