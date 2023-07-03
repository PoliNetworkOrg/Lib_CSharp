namespace PoliNetwork.Core.Utils.LoggerNS;

public class Logger
{
    public LogLevel Level;
    private bool isWriteToFileEnabled = false;
    private string logFilePath = "";

    public Logger(LogLevel? level, string? logFolderPath)
    {
        this.Level = level ?? LogLevel.WARNING;

        if (string.IsNullOrEmpty(logFilePath))
            return;

        this.isWriteToFileEnabled = true;
        this.logFilePath = Path.Join(logFolderPath, DateTime.Now.ToString("yyyyMMdd_HHmmss"), ".log");
    }

    private void WriteToFile(string message)
    {
        if (!isWriteToFileEnabled || string.IsNullOrEmpty(logFilePath))
            return;

        using (StreamWriter writer = new StreamWriter(this.logFilePath, true))
        {
            writer.WriteLine(message);
        }
    }

    private void SetConsoleColor(LogLevel level) {
        switch (level)
        {
            case LogLevel.DEBUG:
                Console.ForegroundColor = ConsoleColor.Cyan;
                break;
                
            case LogLevel.INFO:
                Console.ForegroundColor = ConsoleColor.White;
                break;

            case LogLevel.WARNING:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;

            case LogLevel.ERROR:
                Console.ForegroundColor = ConsoleColor.Red;
                break;

            case LogLevel.EMERGENCY:
                Console.ForegroundColor = ConsoleColor.Magenta;
                break;
        }

    }

    private void Write(LogLevel level, string message)
    {
        if (level < this.Level) return;

        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string messageWithTimestamp = $"{timestamp} [{level}] \t{message}";

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
        this.Write(LogLevel.INFO, message);
    }
}
