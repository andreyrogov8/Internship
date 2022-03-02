using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Application.Telegram
{
    public static class Helper
    {
        public static async Task DeleteMessageAsync(TelegramBotClient telegraBotClient, long telegramId)
        {
            var messages = UserStateStorage.GetUserMessages(telegramId);
            foreach (int message in messages)
            {
                await telegraBotClient.DeleteMessageAsync(telegramId, message);
            }
            UserStateStorage.RemoveMessages(telegramId);
        }

        public static DateTimeOffset GetDate(int month, int day)
        {
            DateTimeOffset Date = new DateTimeOffset(
                                        DateTimeOffset.UtcNow.Year,
                                        month: month,
                                        day: day,
                                        0, 0, 0,
                                        TimeSpan.Zero);
            return Date;
        }
    }
}
