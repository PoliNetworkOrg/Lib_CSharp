#region

using System.Data;
using MySql.Data.MySqlClient;
using PoliNetwork.Db.Objects;

#endregion

namespace PoliNetwork.Db.Utils;

public static class Database
{
    // ReSharper disable once UnusedMember.Global

    /// <summary>
    ///     Execute a query and return the number of rows affected
    /// </summary>
    /// <param name="query">SQL Query</param>
    /// <param name="dbConfig">DBMS Config</param>
    /// <param name="args">Query Args</param>
    /// <returns></returns>
    public static int Execute(string query, DbConfig? dbConfig, Dictionary<string, object?>? args = null)
    {
        var connection = new MySqlConnection(DbConfigUtils.GetConnectionString(dbConfig));

        LoggerDb.Logger?.Invoke(new QueryArgs { Query = query, Args = args });

        var cmd = new MySqlCommand(query, connection);

        if (args != null)
            foreach (var (key, value) in args)
            {
                cmd.Parameters.AddWithValue(key, value);
                query = query.Replace(key, value?.ToString() ?? "NULL");
            }

        dbConfig?.Logger.DbQuery(query);

        OpenConnection(connection);

        int? numberOfRowsAffected = null;
        if (connection.State == ConnectionState.Open) numberOfRowsAffected = cmd.ExecuteNonQuery();

        connection.Close();
        return numberOfRowsAffected ?? -1;
    }

    /// <summary>
    ///     Execute a select query and return the result as a DataTable
    /// </summary>
    /// <param name="query"> SQL Query</param>
    /// <param name="dbConfig"> DBMS Config</param>
    /// <param name="args"> Query Args</param>
    /// <returns></returns>
    public static DataTable? ExecuteSelect(string query, DbConfig? dbConfig, Dictionary<string, object?>? args = null)
    {
        var connection = new MySqlConnection(DbConfigUtils.GetConnectionString(dbConfig));

        LoggerDb.Logger?.Invoke(new QueryArgs { Query = query, Args = args });

        var cmd = new MySqlCommand(query, connection);

        if (args != null)
            foreach (var (key, value) in args)
                cmd.Parameters.AddWithValue(key, value);

        OpenConnection(connection);

        var adapter = new MySqlDataAdapter
        {
            SelectCommand = cmd
        };

        if (connection.State != ConnectionState.Open)
            return default;

        var ret = new DataSet();
        var fr = adapter.Fill(ret);

        adapter.Dispose();
        connection.Close();
        return fr == 0 ? null : ret.Tables[0];
    }

    private static void OpenConnection(IDbConnection connection)
    {
        if (connection.State != ConnectionState.Open) connection.Open();
    }

    // ReSharper disable once UnusedMember.Global
    public static object? GetFirstValueFromDataTable(DataTable? dt)
    {
        try
        {
            return dt?.Rows[0].ItemArray[0];
        }
        catch
        {
            return null;
        }
    }
}