using Telegram.Bot;

namespace PoliNetwork.Telegram.Objects.Configuration;

/// <summary>
///     Telegram Bot configuration class used in PoliNetwork projects
/// </summary>
public abstract class AbstractTelegramConfiguration : TelegramBotClientOptions
{
    protected AbstractTelegramConfiguration(string token, string? baseUrl = null, bool useTestEnvironment = false) : base(token, baseUrl, useTestEnvironment)
    {
    }

    public UpdateMethod? UpdateMethod { get; set; }
}