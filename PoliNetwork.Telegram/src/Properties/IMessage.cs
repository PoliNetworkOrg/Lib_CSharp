namespace PoliNetwork.Telegram.Properties;

public interface IMessage
{
    string? Text { get; }
    IChat Chat { get; }
    IUser? From { get; }
}