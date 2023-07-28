#region

using Newtonsoft.Json;
using PoliNetwork.Core.Data;
using PoliNetwork.Telegram.Objects.Configuration;

#endregion

namespace PoliNetwork.Telegram.Utils.ConfigUtils;

/// <summary>
/// </summary>
public static class TelegramConfigUtils
{
    public static TelegramConfig? LoadOrInitializeConfig(string path)
    {
        if (!File.Exists(path))
        {
            try
            {
                var config = JsonConvert.SerializeObject(new TelegramConfig());
                File.WriteAllText(path, config);
            }
            catch (Exception e)
            {
                GlobalVariables.DefaultLogger.Error(e);
                throw;
            }

            return null;
        }

        var json = File.ReadAllText(path);
        var config2 = JsonConvert.DeserializeObject<TelegramConfig>(json);
        if (config2 == null) GlobalVariables.DefaultLogger.Emergency("Config file could not be deserialized.");
        return config2;
    }
}