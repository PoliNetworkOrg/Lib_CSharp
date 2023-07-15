#region

using Newtonsoft.Json;
using PoliNetwork.Core.Utils.LoggerNS;

#endregion

namespace PoliNetwork.Db.Utils;

[Serializable]
[JsonObject(MemberSerialization.Fields)]
public class DbConfig
{
    public string? Database;
    public string? DatabaseName;
    public string? Host;
    public string? Password;
    public int Port;
    public string? User;
    public Logger Logger;

    public DbConfig(Logger logger) {
        Logger = logger;
    }


    public void FixName()
    {
        if (string.IsNullOrEmpty(DatabaseName))
            DatabaseName = Database;

        if (string.IsNullOrEmpty(Database))
            Database = DatabaseName;
    }


    public string GetConnectionString()
    {
        return string.IsNullOrEmpty(Password)
            ? "server='" + Host + "';user='" + User + "';database='" + DatabaseName + "';port=" + Port
            : "server='" + Host + "';user='" + User + "';database='" + DatabaseName + "';port=" + Port + ";password='" +
              Password + "'";
    }
}