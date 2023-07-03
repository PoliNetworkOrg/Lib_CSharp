namespace PoliNetwork.Core.Utils.LoggerNS;

public class Logger
{
    private readonly LogLevel _level;
    private readonly bool _isWriteToFileEnabled = false;
    private readonly string _logFilePath = "";

    public Logger(LogLevel? level, string? logFolderPath)
    {
        this._level = level ?? LogLevel.WARNING;

        if (string.IsNullOrEmpty(_logFilePath))
            return;

        this._isWriteToFileEnabled = true;
        this._logFilePath = Path.Join(logFolderPath, DateTime.Now.ToString("yyyyMMdd_HHmmss"), ".log");
    }

    private void WriteToFile(string message)
    {
        if (!_isWriteToFileEnabled || string.IsNullOrEmpty(_logFilePath))
            return;

        using var writer = new StreamWriter(this._logFilePath, true);
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
        if (level < this._level) return;

        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        var messageWithTimestamp = $"{timestamp} [{level}] \t{message}";

        WriteToFile(messageWithTimestamp);
        SetConsoleColor(level);
        Console.WriteLine(messageWithTimestamp);
        Console.ResetColor();
    }

    public void Emergency(string message)
    {
        this.Write(LogLevel.EMERGENCY, message);
    }

    public void Error(string message)
    {
        this.Write(LogLevel.ERROR, message);
    }

    public void Warning(string message)
    {
        this.Write(LogLevel.WARNING, message);
    }

    public void Info(string message)
    {
        this.Write(LogLevel.INFO, message);
    }

    public void Debug(string message)
    {
        this.Write(LogLevel.DEBUG, message);
    }
}
