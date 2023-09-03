#region

using PoliNetwork.Telegram.Objects.Updates.Message;
using Telegram.Bot.Types.Enums;

#endregion

namespace PoliNetwork.Telegram.Objects.Updates.Update;

public class UpdateFromBot : IUpdate
{
    public readonly global::Telegram.Bot.Types.Update _update;
    public readonly UpdateType updateType;

    public UpdateFromBot(global::Telegram.Bot.Types.Update update)
    {
        _update = update;
        this.updateType = update.Type;
    }

    public IMessage Message => new MessageFromBot(_update.Message);
}