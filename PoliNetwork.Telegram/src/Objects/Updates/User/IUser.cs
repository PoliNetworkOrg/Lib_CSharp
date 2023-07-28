namespace PoliNetwork.Telegram.Objects.Updates.User;

public interface IUser
{
    long? id { set; get; }
    string? username { set; get; }
    string? firstName { get; set; }
    string? lastName { get; set; }
    bool? ValidName();
}