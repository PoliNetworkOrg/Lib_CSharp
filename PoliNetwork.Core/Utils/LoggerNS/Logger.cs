namespace PoliNetwork.Core.Utils.LoggerNS;

public class Logger
{
    private readonly bool _isWriteToFileEnabled;
    private readonly LogLevel _level;
    private readonly string _logFilePath = "";

    /// <summary>
    /// Instantiate a logger object
    /// </summary>
    /// <param name="level">the level of the logger (default: Warning)</param>
    /// <param name="logFolderPath">the folder where the logs are going to be written into</param>
    /// <param name="isWriteToFileEnabled">if the log are going to be written into files</param>
    public Logger(LogLevel level = LogLevel.WARNING, string? logFolderPath = null, bool isWriteToFileEnabled = true)
    {
        _level = level;

        if (string.IsNullOrEmpty(_logFilePath))
            return;

        this._isWriteToFileEnabled = isWriteToFileEnabled;
        _logFilePath = Path.Join(logFolderPath, DateTime.UtcNow.ToString("yyyyMMdd_HHmmss"), ".log");
    }

    private void WriteToFile(string message)
    {
        if (!_isWriteToFileEnabled || string.IsNullOrEmpty(_logFilePath))
            return;

        using var writer = new StreamWriter(_logFilePath, true);
        writer.WriteLine(message);
    }

    private static void SetConsoleColor(LogLevel level)
    {
        Console.ForegroundColor = level switch
        {
            LogLevel.DEBUG => ConsoleColor.Cyan,
            LogLevel.INFO => ConsoleColor.White,
            LogLevel.WARNING => ConsoleColor.Yellow,
            LogLevel.ERROR => ConsoleColor.Red,
            LogLevel.EMERGENCY => ConsoleColor.Magenta,
            _ => Console.ForegroundColor
        };
    }

    private void Write(LogLevel level, string message)
    {
        if (level < _level) return;

        var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        var messageWithTimestamp = $"{timestamp} [{level}] \t{message}";

        WriteToFile(messageWithTimestamp);
        SetConsoleColor(level);
        Console.WriteLine(messageWithTimestamp);
        Console.ResetColor();
    }

    public void Emergency(string message)
    {
        Write(LogLevel.EMERGENCY, message);
    }

    public void Error(string message)
    {
        Write(LogLevel.ERROR, message);
    }

    public void Warning(string message)
    {
        Write(LogLevel.WARNING, message);
    }

    public void Info(string message)
    {
        Write(LogLevel.INFO, message);
    }

    public void Debug(string message)
    {
        Write(LogLevel.DEBUG, message);
    }

    public void DbQuery(string message)
    {
        Write(LogLevel.DBQUERY, message);
    }
}