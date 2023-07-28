#region

using PoliNetwork.Telegram.Objects.Updates.Chat;

#endregion

namespace PoliNetwork.Telegram.Objects.Updates.Message;

public interface IMessage
{
    string? Text { get; set; }
    IChat Chat { get; set; }
}