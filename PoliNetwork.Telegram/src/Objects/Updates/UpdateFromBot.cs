using Telegram.Bot.Types;

namespace PoliNetwork.Telegram.Objects.Updates;

public class UpdateFromBot : IUpdate
{
    private Update _update;

    public UpdateFromBot(Update update)
    {
        this._update = update;
    }
}