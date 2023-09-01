#region
using PoliNetwork.Telegram.Properties;
using Telegram.Bot.Types;

#endregion

namespace PoliNetwork.Telegram.Options;

public class UpdateMethod
{
    private readonly Action<CancellationToken, IUpdate> _action;

    public UpdateMethod(Action<CancellationToken, IUpdate> action) => _action = action;

    public void Run(Update update, CancellationToken cancellationToken) => _action.Invoke(cancellationToken, new BotUpdate(update));
}