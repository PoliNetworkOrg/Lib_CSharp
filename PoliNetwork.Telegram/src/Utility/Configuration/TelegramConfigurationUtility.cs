using Newtonsoft.Json;
using PoliNetwork.Telegram.Logger;
using PoliNetwork.Telegram.Objects.Configuration;

namespace PoliNetwork.Telegram.Utils.ConfigUtils;

public class TelegramConfigurationUtility
{
    public static readonly FileConfigurationLoader configurationLoader = new JSONFileConfigurationLoader();

    private static AbstractLogger? Logger { get; set; } = new DefaultLogger();

    public abstract class FileConfigurationLoader
    {
        protected abstract void Serialize(string path, AbstractTelegramConfiguration configuration);
        protected abstract AbstractTelegramConfiguration? Deserialize(string path);

        public void InitializeDefaultConfig(string path)
        {
            if (string.IsNullOrEmpty(path) || File.Exists(path))
            {
                throw new ArgumentException($"Invalid file path, file already exists or no argument passed: {path}");
            }

            Deserialize(path);
        }

        public AbstractTelegramConfiguration? LoadConfiguration(string path)
        {
            string configuration = File.ReadAllText(path);
            return Deserialize(configuration);
        }

        public AbstractTelegramConfiguration? LoadOrInitializeConfig(string path)
        {
            AbstractTelegramConfiguration? configuration = null;
            try
            {
                InitializeDefaultConfig(path); /* If non existing configuration file: creates default configuration file */
            }
            catch (ArgumentException ex)
            {
                Logger?.Emergency($"Initializing configuration failed: {ex.Message}");
                configuration = LoadConfiguration(path); /* Configuration exists: loads it */
            }
            catch (JsonSerializationException ex)
            {
                Logger?.Emergency($"Deserialization failed: {ex.Message}");
                throw;
            }

            return configuration;
        }
    }

    public class JSONFileConfigurationLoader : FileConfigurationLoader
    {
        protected override AbstractTelegramConfiguration? Deserialize(string path)
        {
            string configurationJSON = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<AbstractTelegramConfiguration>(configurationJSON);
        }

        protected override void Serialize(string path, AbstractTelegramConfiguration configuration)
        {
            string configurationJSON = JsonConvert.SerializeObject(configuration);
            File.WriteAllText(path, configurationJSON);
        }
    }
}