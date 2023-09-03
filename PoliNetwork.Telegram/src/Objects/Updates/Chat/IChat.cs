#region

using PoliNetwork.Telegram.Objects.Updates.Type;

#endregion

namespace PoliNetwork.Telegram.Objects.Updates.Chat;

public interface IChat
{
    long Id { get; }
    string? Username { get; }
    public ChatType? Type { get; }
    public bool ValidUsername();
}