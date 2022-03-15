using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class SendDateDayKeyboard
    {
        public static InlineKeyboardMarkup BuildKeyboard(CallbackQuery callbackQuery, string callBackData, string typeOfProcess)
        {
            var selectedYear = typeOfProcess == "Start" ?
                UserStateStorage.userInfo[callbackQuery.From.Id].UserDates.StartYear : UserStateStorage.userInfo[callbackQuery.From.Id].UserDates.EndYear;
            var selectedMonth = typeOfProcess == "Start" ?
                UserStateStorage.userInfo[callbackQuery.From.Id].UserDates.StartMonth : UserStateStorage.userInfo[callbackQuery.From.Id].UserDates.EndMonth;

            var currentMonth = DateTime.Now.Month;            
            var endMonth = (currentMonth + 3) % 12;// currentMonth + 3 < 12 ? currentMonth + 3 : currentMonth -= 9; will leave it for understanding

            List <InlineKeyboardButton> buttons = new();
            int numberOfDays = DateTime.DaysInMonth(selectedYear, selectedMonth);
            if (selectedMonth == endMonth) 
            {
                numberOfDays = DateTime.Now.AddMonths(3).Day;
            }

            int startDayInMonth = 1;

            if (typeOfProcess == "Start" && DateTime.Now.Year == selectedYear && DateTime.Now.Month == selectedMonth)
            {
                startDayInMonth = DateTime.Now.Day;
            }

            if (typeOfProcess == "End" 
                && selectedYear == UserStateStorage.userInfo[callbackQuery.From.Id].UserDates.StartYear
                && selectedMonth == UserStateStorage.userInfo[callbackQuery.From.Id].UserDates.StartMonth)
            {
                startDayInMonth = UserStateStorage.userInfo[callbackQuery.From.Id].UserDates.StartDay;
            }

            for (int day = startDayInMonth; day <= numberOfDays; day++)
            {
                buttons.Add(new InlineKeyboardButton(day.ToString()) { CallbackData = day.ToString() });
            }
            buttons.Add(new InlineKeyboardButton($"BACK") { CallbackData = callBackData });
            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, 7);
            return inlineKeyboard;
        }
    }
}
