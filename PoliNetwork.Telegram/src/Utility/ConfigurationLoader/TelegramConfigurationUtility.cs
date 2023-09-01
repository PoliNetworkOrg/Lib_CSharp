using Newtonsoft.Json;
using PoliNetwork.Telegram.Logger;
using PoliNetwork.Telegram.Configuration;

namespace PoliNetwork.Telegram.ConfigurationLoader;

public class TelegramConfigurationUtility
{
    public static readonly FileConfigurationLoader configurationLoader = new JSONFileConfigurationLoader();
}