using Application.Telegram.Models;
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

        public static void StartYearUpdater(CallbackQuery callbackQuery, ref UserInfo userInfo)
        {
            int year = Int32.Parse(callbackQuery.Data);
            userInfo.UserDates.StartYear = year;
        }

        public static void StartMonthUpdater(CallbackQuery callbackQuery, ref UserInfo userInfo)
        {
            int month = Int32.Parse(callbackQuery.Data);            
            userInfo.UserDates.StartMonth = month;
        }

        public static void StartDayUpdater(CallbackQuery callbackQuery, ref UserInfo userInfo)
        {
            int day = Int32.Parse(callbackQuery.Data);
            userInfo.UserDates.StartDay = day;

        }

        public static void EndYearUpdater(CallbackQuery callbackQuery, ref UserInfo userInfo)
        {
            int year = Int32.Parse(callbackQuery.Data);
            userInfo.UserDates.EndYear = year;
        }

        public static void EndMonthUpdater(CallbackQuery callbackQuery, ref UserInfo userInfo)
        {
            int month = Int32.Parse(callbackQuery.Data);
            userInfo.UserDates.EndMonth = month;
        }

        public static void EndDayUpdater(CallbackQuery callbackQuery, ref UserInfo userInfo)
        {
            int day = Int32.Parse(callbackQuery.Data);
            userInfo.UserDates.EndDay = day;

            
        }
    }
}
