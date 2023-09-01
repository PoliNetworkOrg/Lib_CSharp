#region
using PoliNetwork.Telegram.Properties;
using Telegram.Bot.Types;

#endregion

namespace PoliNetwork.Telegram.Options;

public abstract class UpdateMethod
{
    private readonly Action<CancellationToken, BotUpdate> _action;

    protected UpdateMethod(Action<CancellationToken, IUpdate> action) => _action = (Action<CancellationToken, BotUpdate>?)action;

    public void Run(Update update, CancellationToken cancellationToken) => _action.Invoke(cancellationToken, new BotUpdate(update));
}

public interface IUpdate
{
    BotMessage Message { get; }
}