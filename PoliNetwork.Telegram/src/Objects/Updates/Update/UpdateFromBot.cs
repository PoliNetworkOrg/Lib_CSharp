#region

using PoliNetwork.Telegram.Objects.Updates.Message;

#endregion

namespace PoliNetwork.Telegram.Objects.Updates.Update;

public class UpdateFromBot : IUpdate
{
    private readonly global::Telegram.Bot.Types.Update _update;

    public UpdateFromBot(global::Telegram.Bot.Types.Update update)
    {
        _update = update;
    }

    public IMessage Message => new MessageFromBot(_update.Message);
}