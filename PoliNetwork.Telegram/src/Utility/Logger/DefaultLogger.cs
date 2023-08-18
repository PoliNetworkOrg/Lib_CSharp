namespace PoliNetwork.Telegram.Logger;

public class DefaultLogger : AbstractLogger
{
    public override void WriteToFile(string content)
    {
        if (!logConfiguration.IsWritable()) return;
        using var writer = new StreamWriter(logConfiguration.path, true);
        writer.WriteLine(content);
    }

    protected override void WriteLogic(LogLevel level, object message)
    {
        var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        var messageMetadata = $"{timestamp} [{level}] \t";

        WriteToFile($"{messageMetadata}: {message}");
        Console.Write(messageMetadata);
        Console.WriteLine(message);
        Console.ResetColor();
    }
}