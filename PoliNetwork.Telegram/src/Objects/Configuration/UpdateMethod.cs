#region

using PoliNetwork.Telegram.Objects.Updates.Update;
using Telegram.Bot.Types;

#endregion

namespace PoliNetwork.Telegram.Objects.Configuration;

public class UpdateMethod
{
    private readonly Action<CancellationToken, IUpdate> _action;

    public UpdateMethod(Action<CancellationToken, IUpdate> action)
    {
        _action = action;
    }

    public void Run(Update bUpdate, CancellationToken cancellationToken)
    {
        _action.Invoke(cancellationToken, new UpdateFromBot(bUpdate));
    }
}