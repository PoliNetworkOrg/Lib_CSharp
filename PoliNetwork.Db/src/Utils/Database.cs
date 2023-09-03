#region

using System.Data;
using System.Data.SQLite;
using MySql.Data.MySqlClient;
using PoliNetwork.Db.Objects;
using DbType = PoliNetwork.Db.Objects.DbType;

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
    public static int? Execute(string query, DbConfig? dbConfig, Dictionary<string, object?>? args = null)
    {
        return dbConfig?.DbType switch
        {
            DbType.MYSQL => ExecuteMySql(query, dbConfig, args),
            DbType.SQLITE => ExecuteSqlLite(query, dbConfig, args),
            _ => null
        };
    }

    private static int ExecuteMySql(string query, DbConfig dbConfig, Dictionary<string, object?>? args)
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

        dbConfig.Logger.DbQuery(query);

        OpenConnection(connection);

        int? numberOfRowsAffected = null;
        if (connection.State == ConnectionState.Open) numberOfRowsAffected = cmd.ExecuteNonQuery();

        connection.Close();
        return numberOfRowsAffected ?? -1;
    }

    private static int? ExecuteSqlLite(string query, DbConfig dbConfig, Dictionary<string, object?>? args)
    {
        var conn = GetConnString(dbConfig);
        if (string.IsNullOrEmpty(conn))
            return null;

        return ExecuteNonQuerySqlLite(conn, query);
    }


    private static int ExecuteNonQuerySqlLite(string connectionString, string query)
    {

        var rowsAffected = 0;

        try
        {
            using SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                rowsAffected = command.ExecuteNonQuery();
            }

            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return rowsAffected;
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
        return dbConfig?.DbType switch
        {
            DbType.MYSQL => ExecuteSelectMySql(query, dbConfig, args),
            DbType.SQLITE => ExecuteSelectSqlite(query, dbConfig, args),
            _ => null
        };
    }

    private static DataTable? ExecuteSelectSqlite(string query, DbConfig dbConfig, Dictionary<string, object?>? args)
    {
        var connectionString = GetConnString(dbConfig);
        return string.IsNullOrEmpty(connectionString) ? null : ExecuteQueryAndGetDataTable(connectionString, query);
    }

    private static string? GetConnString(DbConfig dbConfig)
    {
        if (dbConfig.Path == null) return null;
        var path = FindFile(dbConfig.Path, ".");
        if (string.IsNullOrEmpty(path)) return null;
        var connectionString = "Data Source=" + path + ";Version=3;";
        return connectionString;
    }


    private static string? FindFile(string fileName, string? startFolder)
    {
        if (string.IsNullOrEmpty(startFolder))
            return null;
        
        // Check if the file exists in the current folder
        string? filePath = Path.Combine(startFolder, fileName);
        if (File.Exists(filePath))
        {
            return filePath;
        }

        // Check if the file exists in the parent folder (if not root)
        var parentFolder = Directory.GetParent(startFolder)?.FullName;
        if (!string.IsNullOrEmpty(parentFolder))
        {
            filePath = FindFile(fileName, parentFolder);
            if (!string.IsNullOrEmpty(filePath))
            {
                return filePath;
            }
        }

        // Check if the file exists in sibling folders
        if (parentFolder == null) return null;
        string?[] siblingFolders = Directory.GetDirectories(parentFolder);
        foreach (string? siblingFolder in siblingFolders)
        {
            filePath = FindFile(fileName, siblingFolder);
            if (!string.IsNullOrEmpty(filePath))
            {
                return filePath;
            }
        }

        // File not found
        return null;
    }


    private static DataTable ExecuteQueryAndGetDataTable(string? connectionString, string query)
    {
        DataTable dataTable = new DataTable();

        using SQLiteConnection connection = new SQLiteConnection(connectionString);
        connection.Open();

        using (SQLiteCommand command = new SQLiteCommand(query, connection))
        {
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                dataTable.Load(reader);
            }
        }

        connection.Close();

        return dataTable;
    }


    private static DataTable? ExecuteSelectMySql(string query, DbConfig dbConfig, Dictionary<string, object?>? args)
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

    // ReSharper disable once UnusedMember.Global
    public static long? GetIntFromColumn(DataRow dr, string columnName)
    {
        var o = dr[columnName];
        if (o is null or DBNull) return null;

        try
        {
            return Convert.ToInt64(o);
        }
        catch
        {
            return null;
        }
    }
}