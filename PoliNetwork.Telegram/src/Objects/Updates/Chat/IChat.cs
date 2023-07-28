#region

using PoliNetwork.Telegram.Objects.Updates.Type;

#endregion

namespace PoliNetwork.Telegram.Objects.Updates.Chat;

public interface IChat
{
    long? Id { get; set; }
    string? Username { get; set; }
    public ChatType? Type { get; set; }
    public bool ValidUsername();
}