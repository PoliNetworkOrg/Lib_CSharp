using Newtonsoft.Json;
using PoliNetwork.Core.Data;

namespace PoliNetwork.Telegram.Objects.Configuration
{
    
    /// <summary>
    /// Telegram Bot configuration class used in PoliNetwork projects
    /// </summary>
    public class TelegramConfig
    {
        public string Token { get; set; } = "Insert token here";
        
        public string? BaseUrl { get; set; }

        public bool UseTestEnvironment { get; set; } = false;
        
    }
    
}