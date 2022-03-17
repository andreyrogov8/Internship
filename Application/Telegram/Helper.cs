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
                DateTimeOffset EndDate = new DateTimeOffset(
                            year: UserStateStorage.userInfo[callbackQuery.From.Id].UserDates.EndYear,
                            month: UserStateStorage.userInfo[callbackQuery.From.Id].UserDates.EndMonth,
                            day: UserStateStorage.userInfo[callbackQuery.From.Id].UserDates.EndDay,
                            0, 0, 0,
                            TimeSpan.Zero);
                //finding recurring day
                while (!Date.ToString("dddd").Equals(UserStateStorage.userInfo[callbackQuery.From.Id].RecurringDay))
                {
                    Date = Date.AddDays(1);
                    //if we already reached end date and recurring day was not found then we are setting RecurringDayNotFount to true
                    if (Date == EndDate)
                    {
                        UserStateStorage.userInfo[callbackQuery.From.Id].RecurringDayWasNotFound = true;
                        break;
                    }
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
                //checking if reccuring day was found during GetStartDate procedure
                if (UserStateStorage.userInfo[callbackQuery.From.Id].RecurringDayWasNotFound)
                {
                    return Date;
                }
                while (!Date.ToString("dddd").Equals(UserStateStorage.userInfo[callbackQuery.From.Id].RecurringDay))
                {
                    Date = Date.AddDays(-1);
                }
            }
            return Date;
        }

        public static List<DateTimeOffset> GetRecurringDays(DateTimeOffset start, DateTimeOffset end)
        {
            List<DateTimeOffset> days = new List<DateTimeOffset>();
            DateTimeOffset day = start;
            while (day <= end)
            {
                days.Add(day);
                day = day.AddDays(7);
            }
            return days;
        }
    }
}
