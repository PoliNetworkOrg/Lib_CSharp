namespace PoliNetwork.Telegram.Objects.Updates;

public interface IUpdate
{
    IMessage Message { get; set; }
}

public interface IMessage
{
    string? Text { get; set; }
    IChat Chat { get; set; }
}

public interface IChat
{
    long? Id { get; set; }
}