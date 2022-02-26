using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Application.Telegram.DataCommands
{
    public static class DateHelper
    {
        public static void BookingStartMonthUpdater(CallbackQuery callbackQuery)
        {
            int month = Int32.Parse(callbackQuery.Data);
            var booking = UserStateStorage.userInfo[callbackQuery.From.Id].Booking;
            var currentStartDate = booking.StartDate;
            booking.StartDate = new DateTimeOffset(
                DateTimeOffset.UtcNow.Year,
                month,
                currentStartDate.Day,
                0, 0, 0,
                currentStartDate.Offset);
        }
        public static void BookingStartDayUpdater(CallbackQuery callbackQuery)
        {
            int day = Int32.Parse(callbackQuery.Data);
            var booking = UserStateStorage.userInfo[callbackQuery.From.Id].Booking;
            var currentStartDate = booking.StartDate;
            booking.StartDate = new DateTimeOffset(
                DateTimeOffset.UtcNow.Year,
                currentStartDate.Month,
                day,
                0, 0, 0,
                currentStartDate.Offset);
        }

        public static void BookingEndMonthUpdater(CallbackQuery callbackQuery)
        {
            int month = Int32.Parse(callbackQuery.Data);
            var booking = UserStateStorage.userInfo[callbackQuery.From.Id].Booking;
            var currentStartDate = booking.StartDate;
            booking.EndDate = new DateTimeOffset(
                DateTimeOffset.UtcNow.Year,
                month,
                currentStartDate.Day,
                0, 0, 0,
                currentStartDate.Offset);
        }
        public static void BookingEndDayUpdater(CallbackQuery callbackQuery)
        {
            int day = Int32.Parse(callbackQuery.Data);
            var booking = UserStateStorage.userInfo[callbackQuery.From.Id].Booking;
            var currentStartDate = booking.StartDate;
            booking.EndDate = new DateTimeOffset(
                DateTimeOffset.UtcNow.Year,
                currentStartDate.Month,
                day,
                0, 0, 0,
                currentStartDate.Offset);
        }
    }
}
