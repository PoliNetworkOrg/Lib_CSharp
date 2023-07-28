using Newtonsoft.Json;
using PoliNetwork.Core.Data;
using PoliNetwork.Core.Utils.LoggerNS;

namespace PoliNetwork.Db.Objects;


[Serializable]
[JsonObject(MemberSerialization.Fields)]
public class DbConfig
{
    public string? Database;
    public string? DatabaseName;
    public string? Host;
    public Logger Logger;
    public string? Password;
    public int Port;
    public string? User;

    public DbConfig(Logger? logger)
    {
        Logger = logger ?? GlobalVariables.DefaultLogger;
    }


    public void FixName()
    {
        if (string.IsNullOrEmpty(DatabaseName))
            DatabaseName = Database;

        if (string.IsNullOrEmpty(Database))
            Database = DatabaseName;
    }
}
