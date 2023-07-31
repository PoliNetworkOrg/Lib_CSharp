#region

using PoliNetwork.Telegram.Objects.Updates.Update;
using Telegram.Bot.Types;

#endregion

namespace PoliNetwork.Telegram.Objects.Configuration;

public class UpdateMethod
{
    private readonly Action<CancellationToken, Update> _action;

    public UpdateMethod(Action<CancellationToken, Update> action) => _action = action;

    public void Run(Update update, CancellationToken cancellationToken) => _action.Invoke(cancellationToken, update);
}