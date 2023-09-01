using PoliNetwork.Telegram.Bot.Bots;
using PoliNetwork.Telegram.Properties;

namespace PoliNetwork.Telegram.Bot.Functionality
{
    public interface IEcho
    {
        public void Echo(IMessage message, CancellationToken cancellationToken);
    }
}