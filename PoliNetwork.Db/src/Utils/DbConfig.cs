#region

using Newtonsoft.Json;
using PoliNetwork.Core.Data;
using PoliNetwork.Db.Objects;

#endregion

namespace PoliNetwork.Db.Utils;

public static class DbConfigUtils
{
    /// <summary>
    ///     Gets the connection string from a DbConfig object.
    /// </summary>
    /// <param name="config"></param>
    /// <returns></returns>
    public static string GetConnectionString(DbConfig config)
    {
        return string.IsNullOrEmpty(config.Password)
            ? "server='" + config.Host + "';user='" + config.User + "';database='" + config.DatabaseName + "';port=" +
              config.Port
            : "server='" + config.Host + "';user='" + config.User + "';database='" + config.DatabaseName + "';port=" +
              config.Port + ";password='" +
              config.Password + "'";
    }

    /// <summary>
    ///     Creates a new config file if it does not exist, otherwise it loads it.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static DbConfig? LoadOrInitializeConfig(string path)
    {
        if (!File.Exists(path))
        {
            try
            {
                var config = JsonConvert.SerializeObject(new DbConfig(null));
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
        var config2 = JsonConvert.DeserializeObject<DbConfig>(json);
        if (config2 == null) GlobalVariables.DefaultLogger.Emergency("Config file could not be deserialized.");

        return config2;
    }
}