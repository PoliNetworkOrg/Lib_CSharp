namespace PoliNetwork.Telegram.Utility.Logger;

public abstract class AbstractLogger
{
    public enum LogLevel
    {
        EMERGENCY = 6,
        ERROR = 5,
        WARNING = 4,
        INFO = 3,
        DEBUG = 2,
        DBQUERY = 1
    }

    public class LogConfiguration
    {
        public readonly LogLevel Level;
        public readonly string path = "";

        public bool IsWritable()
        {
            return !string.IsNullOrEmpty(path);
        }

        public LogConfiguration(LogLevel logLevel = LogLevel.WARNING, string? logFilePath = null)
        {
            if (!IsWritable()) return;

            Level = logLevel;
            path = Path.Join(logFilePath, DateTime.UtcNow.ToString("yyyyMMdd_HHmmss"), ".log");
        }
    }

    protected readonly LogConfiguration logConfiguration;

    public AbstractLogger(LogConfiguration? LogConfiguration = null)
    {
        logConfiguration = LogConfiguration ?? new LogConfiguration();
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

    public abstract void WriteToFile(string message);

    protected abstract void WriteLogic(LogLevel level, object message);


    protected void Write(LogLevel level, object message)
    {
        if (level < logConfiguration.Level) return;
        SetConsoleColor(level);
        WriteLogic(level, message);
    }

    public void Emergency(object message)
    {
        Write(LogLevel.EMERGENCY, message);
    }

    public void Error(object message)
    {
        Write(LogLevel.ERROR, message);
    }
    
    //function to handle multiple objects by splitting them into multiple Write calls
    public void Error(params object[] messages)
    {
        foreach (var message in messages)
        {
            Write(LogLevel.ERROR, message);
        }
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

    internal void Info(string message)
    {
        throw new NotImplementedException();
    }
}