namespace PoliNetwork.Telegram.Properties;

public interface IChat
{
    long Id { get; }
    string? Username { get; }
    public ChatType? Type { get; }
    public bool ValidUsername();
}