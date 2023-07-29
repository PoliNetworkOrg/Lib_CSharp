#region

using PoliNetwork.Telegram.Objects.Updates.Message;

#endregion

namespace PoliNetwork.Telegram.Objects.Updates.Update;

public interface IUpdate
{
    IMessage Message { get; }
}