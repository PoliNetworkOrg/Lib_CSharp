#region

using Telegram.Bot.Types;

#endregion

namespace PoliNetwork.Telegram.Objects.Updates;

public class UpdateFromBot : IUpdate
{
    private Update _update;

    public UpdateFromBot(Update update)
    {
        _update = update;
        Message = new MessageFromBot(update.Message);
    }

    public IMessage Message { get; set; }
}