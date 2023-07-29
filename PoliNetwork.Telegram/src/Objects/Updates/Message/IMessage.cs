#region

using PoliNetwork.Telegram.Objects.Updates.Chat;
using PoliNetwork.Telegram.Objects.Updates.User;

#endregion

namespace PoliNetwork.Telegram.Objects.Updates.Message;

public interface IMessage
{
    string? Text { get; }
    IChat Chat { get; }
    IUser From { get; }
}