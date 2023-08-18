namespace PoliNetwork.Telegram.Options;

public abstract class AbstractTelegramBotOptions : TelegramBotClientOptions
{
    protected AbstractTelegramBotOptions(string token, string? baseUrl = null, bool useTestEnvironment = false) : base(token, baseUrl, useTestEnvironment) { }
    public UpdateMethod? UpdateMethod { get; set; }
}