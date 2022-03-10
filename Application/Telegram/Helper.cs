using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

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

        public static DateTimeOffset GetStartDate(CallbackQuery callbackQuery)
        {
            DateTimeOffset Date = new DateTimeOffset(
                                        year: UserStateStorage.userInfo[callbackQuery.From.Id].UserDates.StartYear,
                                        month: UserStateStorage.userInfo[callbackQuery.From.Id].UserDates.StartMonth,
                                        day: UserStateStorage.userInfo[callbackQuery.From.Id].UserDates.StartDay,
                                        0, 0, 0,
                                        TimeSpan.Zero);
            if (UserStateStorage.userInfo[callbackQuery.From.Id].RecurringDay is not null)
            {
                while (!Date.ToString("dddd").Equals(UserStateStorage.userInfo[callbackQuery.From.Id].RecurringDay))
                {
                    Date = Date.AddDays(1);
                }
            }
            return Date;
        }

        public static DateTimeOffset GetEndDate(CallbackQuery callbackQuery)
        {
            DateTimeOffset Date = new DateTimeOffset(
                                        year: UserStateStorage.userInfo[callbackQuery.From.Id].UserDates.EndYear,
                                        month: UserStateStorage.userInfo[callbackQuery.From.Id].UserDates.EndMonth,
                                        day: UserStateStorage.userInfo[callbackQuery.From.Id].UserDates.EndDay,
                                        0, 0, 0,
                                        TimeSpan.Zero);
            if (UserStateStorage.userInfo[callbackQuery.From.Id].RecurringDay is not null)
            {
                while (!Date.ToString("dddd").Equals(UserStateStorage.userInfo[callbackQuery.From.Id].RecurringDay))
                {
                    Date = Date.AddDays(-1);
                }
            }
            return Date;
        }

    }
}
