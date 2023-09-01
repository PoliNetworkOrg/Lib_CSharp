using Newtonsoft.Json;
using PoliNetwork.Telegram.Options;

namespace PoliNetwork.Telegram.ConfigurationLoader;

public class JSONFileConfigurationLoader : FileConfigurationLoader
{
    protected override AbstractTelegramBotOptions? Deserialize(string path)
    {
        string configurationJSON = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<AbstractTelegramBotOptions>(configurationJSON);
    }

    protected override void Serialize(string path, AbstractTelegramBotOptions configuration)
    {
        string configurationJSON = JsonConvert.SerializeObject(configuration);
        File.WriteAllText(path, configurationJSON);
    }
}