namespace PoliNetwork.Telegram.Objects.Updates.User;

public class BotUser : global::Telegram.Bot.Types.User, IUser
{
    public static readonly long MIN_NAME_LENGTH = 2;

    public BotUser() : base() { }

    public bool? ValidName() => !string.IsNullOrEmpty(FirstName) && FirstName.Length > MIN_NAME_LENGTH;
}