using Telegram.Bot.Types;

namespace PoliNetwork.Telegram.Objects.Bot.Functionality
{
    /* Is Echo a functionality or a Utility? */
    public interface IEcho
    {
        public void Echo(Message message, CancellationToken cancellationToken);
    }
}