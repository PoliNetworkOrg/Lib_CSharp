using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PoliNetwork.Db.Objects;

[Serializable]
[JsonObject(MemberSerialization.Fields, NamingStrategyType = typeof(CamelCaseNamingStrategy))]
public class QueryArgs
{
    public string? Query;
    public Dictionary<string, object?>? Args;
}