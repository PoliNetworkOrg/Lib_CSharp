using Telegram.Bot;

namespace PoliNetwork.Telegram.Objects.Configuration;

/// <summary>
///     Telegram Bot configuration class used in PoliNetwork projects
/// </summary>
public class TelegramConfiguration : AbstractTelegramConfiguration
{
    public TelegramConfiguration(string token, string? baseUrl = null, bool useTestEnvironment = false) : base(token, baseUrl, useTestEnvironment)
    { }
}