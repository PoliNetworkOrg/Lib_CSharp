using PoliNetwork.Telegram.Objects.Updates;
using Telegram.Bot.Types;

namespace PoliNetwork.Telegram.Objects;

public class UpdateMethod
{
    private readonly Action<CancellationToken, IUpdate> _action;

    public UpdateMethod(Action<CancellationToken, IUpdate>  action)
    {
        this._action = action;
    }

    public void Run(Update bUpdate, CancellationToken cancellationToken)
    {
        this._action.Invoke(cancellationToken, new UpdateFromBot(bUpdate));
    }
}