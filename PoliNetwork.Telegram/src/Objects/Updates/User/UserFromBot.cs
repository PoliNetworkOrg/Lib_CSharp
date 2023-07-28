namespace PoliNetwork.Telegram.Objects.Updates.User;

public class UserFromBot : IUser
{
    public UserFromBot(global::Telegram.Bot.Types.User? updateMessageFrom)
    {
        id = updateMessageFrom?.Id;
        username = updateMessageFrom?.Username;
    }

    public long? id { get; set; }
    public string? username { get; set; }
}