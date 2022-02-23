using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Application.Telegram
{
    public static class TelegramMessages
    {
        public static async Task Delete(TelegramBotClient telegraBotClient, long telegramId)
        {
            var messages = UserStateStorage.GetUserMessages(telegramId);
            foreach (int message in messages)
            {
                await telegraBotClient.DeleteMessageAsync(telegramId, message);
            }
            UserStateStorage.RemoveMessages(telegramId);
        }
    }
}
