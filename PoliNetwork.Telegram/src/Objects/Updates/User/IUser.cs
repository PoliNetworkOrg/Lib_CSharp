namespace PoliNetwork.Telegram.Objects.Updates.User;

public interface IUser
{
    long? id { set; get; }
    string? username { set; get; }
}