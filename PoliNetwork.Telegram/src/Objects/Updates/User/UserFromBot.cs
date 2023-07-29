namespace PoliNetwork.Telegram.Objects.Updates.User;

public class UserFromBot : IUser
{
    private readonly global::Telegram.Bot.Types.User? _user;

    public UserFromBot(global::Telegram.Bot.Types.User? updateMessageFrom)
    {
        _user = updateMessageFrom;
    }

    public long? Id => _user?.Id;

    public string? Username => _user?.Username;

    public string? FirstName => _user?.FirstName;

    public string? LastName => _user?.LastName;

    public bool? ValidName()
    {
        return !string.IsNullOrEmpty(_user?.FirstName) && FirstName?.Length > 2;
    }
}