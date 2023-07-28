namespace PoliNetwork.Telegram.Objects.Updates.User;

public class UserFromBot : IUser
{
    public UserFromBot(global::Telegram.Bot.Types.User? updateMessageFrom)
    {
        id = updateMessageFrom?.Id;
        username = updateMessageFrom?.Username;
        firstName = updateMessageFrom?.FirstName;
        lastName = updateMessageFrom?.LastName;
    }

    public long? id { get; set; }
    public string? username { get; set; }
    public string? firstName { get; set; }
    public string? lastName { get; set; }

    public bool? ValidName()
    {
        return !string.IsNullOrEmpty(firstName) && firstName.Length > 2;
    }
}