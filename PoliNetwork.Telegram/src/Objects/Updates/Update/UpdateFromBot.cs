#region

using PoliNetwork.Telegram.Objects.Updates.Message;

#endregion

namespace PoliNetwork.Telegram.Objects.Updates.Update;

public class UpdateFromBot : IUpdate
{
    private global::Telegram.Bot.Types.Update _update;

    public UpdateFromBot(global::Telegram.Bot.Types.Update update)
    {
        _update = update;
        Message = new MessageFromBot(update.Message);
    }

    public IMessage Message { get; set; }
}