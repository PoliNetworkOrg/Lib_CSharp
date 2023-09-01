
using PoliNetwork.Telegram.ConfigurationLoader;

namespace PoliNetwork.Telegram.Utility.ConfigurationLoader;

public class TelegramConfigurationUtility
{
    public static readonly FileConfigurationLoader ConfigurationLoader = new JSONFileConfigurationLoader();



}