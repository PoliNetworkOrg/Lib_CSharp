#region

using System.Globalization;

#endregion

namespace PoliNetwork.Core.Logging;

public class Logger
{
    private static readonly object LogFileLock = new();
    private static int _logLevel = 3;
    private static string _logDirPath = "./data";
    private static string _logFileName = "output.log";

    public Logger(string logDirPath, string logFileName, LogSeverityLevel logLevel) {
        _logDirPath = logDirPath;
        _logFileName = logFileName;
        _logLevel = (int)logLevel;
        if (!Directory.Exists(_logDirPath)) 
            Directory.CreateDirectory(_logDirPath);

        if (!File.Exists(_logDirPath + "/" + _logFileName))
        {
            FileInfo file = new(_logDirPath + "/" + _logFileName);
            file.Directory?.Create();
            File.WriteAllText(file.FullName, "");
        }
    }

    public static void WriteLine(object? log, LogSeverityLevel logSeverityLevel = LogSeverityLevel.Info)
    {
        if (log == null || string.IsNullOrEmpty(log.ToString()) ||
            (int)logSeverityLevel > _logLevel) return;

        try
        {
            Console.ForegroundColor = logSeverityLevel switch
            {
                LogSeverityLevel.Critical => ConsoleColor.Red,
                LogSeverityLevel.Error => ConsoleColor.DarkRed,
                LogSeverityLevel.Warning => ConsoleColor.Yellow,
                _ => Console.ForegroundColor
            };

            Console.WriteLine(GetTime() + " | " + logSeverityLevel + " | " + log);
            var log1 = log.ToString();
            Directory.CreateDirectory(_logDirPath);

            if (!File.Exists(_logDirPath + "/" + _logFileName))
            {
                FileInfo file = new(_logDirPath + "/" + _logFileName);
                file.Directory?.Create();
                File.WriteAllText(file.FullName, "");
            }

            Console.ResetColor();

            try
            {
                lock (LogFileLock)
                {
                    File.AppendAllLinesAsync(_logDirPath + "/" + _logFileName, new[]
                    {
                        "#@#LOG ENTRY#@#" + GetTime() + " | " + logSeverityLevel + " | " + log1
                    });
                }
            }
            catch (Exception e)
            {
                CriticalError(e, log);
            }
        }
        catch (Exception e)
        {
            CriticalError(e, log);
        }
    }

    private static void CriticalError(Exception e, object? log)
    {
        try
        {
            Console.WriteLine("#############1#############");
            Console.WriteLine("CRITICAL ERROR IN LOGGER APPLICATION! NOTIFY ASAP!");
            Console.WriteLine(e);
            Console.WriteLine("#############2#############");
            if (log == null)
                Console.WriteLine("[null]");
            else
                Console.WriteLine(log);

            Console.WriteLine("#############3#############");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private static string GetTime()
    {
        return DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
    }

}