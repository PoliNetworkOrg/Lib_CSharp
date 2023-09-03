#region

using PoliNetwork.Telegram.Objects.Updates.Message;
using Telegram.Bot.Types.Enums;

#endregion

namespace PoliNetwork.Telegram.Objects.Updates.Update;

public interface IUpdate
{
    IMessage Message { get; }
}