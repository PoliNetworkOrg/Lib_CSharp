#region

using PoliNetwork.Db.Objects;

#endregion

namespace PoliNetwork.Db.Utils;

public static class LoggerDb
{
    /***
     * Set this at the start of your program
     */
    public static Action<QueryArgs>? Logger;
}