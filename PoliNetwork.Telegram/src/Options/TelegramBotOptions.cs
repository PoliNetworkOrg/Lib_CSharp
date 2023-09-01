namespace PoliNetwork.Telegram.Options;

public class TelegramBotOptions : AbstractTelegramBotOptions
{
    public TelegramBotOptions(string token, string? baseUrl = null, bool useTestEnvironment = false) : base(token, baseUrl, useTestEnvironment)
    { }
}