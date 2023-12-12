namespace PoliNetwork.Core.Utils.LoggerNS;

public class Logger
{
    private LogConfig _logConfig;

    /// <summary>
    ///     Instantiate a logger object
    /// </summary>
    /// <param name="logConfig">config for the logger</param>
    public Logger(LogConfig? logConfig = null)
    {
        _logConfig = logConfig ?? new LogConfig();
    }

    private void WriteToFile(string message)
    {
        if (!_logConfig.CanWriteToFile()) return;
        using var writer = new StreamWriter(_logConfig.LogFilePath, true);
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
            LogLevel.DBQUERY => ConsoleColor.Blue,
            _ => Console.ForegroundColor
        };
    }

    private void Write(LogLevel level, object message)
    {
        if (level > _logConfig.Level) return;

        var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        var messageMetadata = $"{timestamp} [{level}] \t";

        WriteToFile(messageMetadata + message);
        SetConsoleColor(level);
        Console.Write(messageMetadata);
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void Emergency(object message)
    {
        Write(LogLevel.EMERGENCY, message);
    }

    public void Error(object message)
    {
        Write(LogLevel.ERROR, message);
    }

    public void Warning(object message)
    {
        Write(LogLevel.WARNING, message);
    }

    public void Info(object message)
    {
        Write(LogLevel.INFO, message);
    }

    public void Debug(object message)
    {
        Write(LogLevel.DEBUG, message);
    }

    public void DbQuery(string message)
    {
        Write(LogLevel.DBQUERY, message);
    }


    public void SetLogConfig(LogConfig logConfig)
    {
        _logConfig = logConfig;
    }
}