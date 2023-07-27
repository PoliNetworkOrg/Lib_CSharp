using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace PoliNetwork.Telegram.Objects;

public class TelegramBotWrapper
{
    private readonly TelegramBotClient? _telegramBotClient;

    public TelegramBotWrapper(string token)
    {
        this._telegramBotClient = new TelegramBotClient(token);
    }

    private void SendMessage(long chatId, string text)
    {
        this._telegramBotClient?.SendTextMessageAsync(chatId, text);
    }


    public void CheckUpdatesAndRun()
    {
        var updates = this._telegramBotClient?.GetUpdatesAsync();
        updates?.Wait();
        var updatesList = updates?.Result;
        if (updatesList == null) return;
        foreach (var update in updatesList)
        {
            if (update.Type != UpdateType.Message) continue;
            var variableMessage = update.Message;
            var variableMessageFrom = variableMessage?.From;
            if (variableMessageFrom == null) continue;
            if (variableMessage?.Text != null)
                this.SendMessage(variableMessageFrom.Id, variableMessage.Text);
        }
    }
} 