using Newtonsoft.Json;
using PoliNetwork.Telegram.Logger;
using PoliNetwork.Telegram.Options;

namespace PoliNetwork.Telegram.ConfigurationLoader;

public abstract class FileConfigurationLoader
{
    protected abstract void Serialize(string path, AbstractTelegramBotOptions configuration);
    protected abstract AbstractTelegramBotOptions? Deserialize(string path);

    public void InitializeDefaultConfig(string path, AbstractLogger? logger)
    {
        if (string.IsNullOrEmpty(path) || File.Exists(path))
        {
            throw new ArgumentException($"Invalid file path, file already exists or no argument passed: {path}");
        }

        /* What to do here? */
    }

    public AbstractTelegramBotOptions? LoadConfiguration(string path)
    {
        string configuration = File.ReadAllText(path);
        return Deserialize(configuration);
    }

    public AbstractTelegramBotOptions? LoadOrInitializeConfig(string path, AbstractLogger? logger)
    {
        AbstractTelegramBotOptions? configuration = null;
        try
        {
            InitializeDefaultConfig(path, logger); 
        }
        catch (ArgumentException ex)
        {
            logger?.Emergency($"Initializing configuration failed: {ex.Message}");
            configuration = LoadConfiguration(path); /* Configuration exists: loads it */
        }
        catch (JsonSerializationException ex)
        {
            logger?.Emergency($"Deserialization failed: {ex.Message}");
            throw;
        }

        return configuration;
    }
}