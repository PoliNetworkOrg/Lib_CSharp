namespace PoliNetwork.Core.Utils.LoggerNS;

public class LogConfig
{
    private readonly bool _isWriteToFileEnabled;
    public readonly LogLevel Level;
    public readonly string LogFilePath = "";

    public LogConfig(LogLevel logLevel = LogLevel.WARNING, bool isWriteToFileEnabled = true, string? logFilePath = null)
    {
        if (string.IsNullOrEmpty(logFilePath))
            return;

        this.Level = logLevel;
        this._isWriteToFileEnabled = isWriteToFileEnabled;
        LogFilePath = Path.Join(logFilePath, DateTime.UtcNow.ToString("yyyyMMdd_HHmmss"), ".log");
    }

    public bool CanWriteToFile()
    {
        return _isWriteToFileEnabled && !string.IsNullOrEmpty(this.LogFilePath);
    }
}