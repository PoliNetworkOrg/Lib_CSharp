#region

using PoliNetwork.Telegram.Objects.Bot;
using PoliNetwork.Telegram.Objects.Updates.Update;
using Telegram.Bot.Types;

#endregion

namespace PoliNetwork.Telegram.Objects.Configuration;

public class UpdateMethod
{
    private readonly Action<CancellationToken, IUpdate, TelegramBot> _action;

    public UpdateMethod(Action<CancellationToken, IUpdate, TelegramBot> action)
    {
        _action = action;
    }

    public void Run(Update bUpdate, CancellationToken cancellationToken, TelegramBot telegramBot)
    {
        var updateFromBot = new UpdateFromBot(bUpdate);
        _action.Invoke(cancellationToken, updateFromBot, telegramBot);
    }
}