namespace PoliNetwork.Telegram.Properties;

public interface IUser
{
    long Id { get; }
    string? Username { get; }
    string? FirstName { get; }
    string? LastName { get; }

    bool? ValidName();
}