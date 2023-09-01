#region
using PoliNetwork.Telegram.Properties;

#endregion

namespace PoliNetwork.Telegram.Options;

public abstract class UpdateMethod
{
    protected UpdateMethod(Action<CancellationToken, IUpdate> action, Action<CancellationToken, BotUpdate> action1, Action<CancellationToken, BotUpdate> action2)
    {
    }
}

public interface IUpdate
{
}