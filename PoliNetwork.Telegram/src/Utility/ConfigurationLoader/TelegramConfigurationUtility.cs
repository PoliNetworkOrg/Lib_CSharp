<<<<<<< HEAD
using PoliNetwork.Telegram.ConfigurationLoader;

namespace PoliNetwork.Telegram.Utility.ConfigurationLoader;

public class TelegramConfigurationUtility
{
    public static readonly FileConfigurationLoader ConfigurationLoader = new JSONFileConfigurationLoader();
=======
using Newtonsoft.Json;
using PoliNetwork.Telegram.Logger;
using PoliNetwork.Telegram.Configuration;

namespace PoliNetwork.Telegram.ConfigurationLoader;

public class TelegramConfigurationUtility
{
    public static readonly FileConfigurationLoader configurationLoader = new JSONFileConfigurationLoader();
>>>>>>> v3
}