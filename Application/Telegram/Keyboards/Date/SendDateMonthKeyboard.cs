using Application.Features.CountriesFeature.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class SendDateMonthKeyboard
    {
        public static InlineKeyboardMarkup BuildKeyboard(CallbackQuery callbackQuery,string callBackData, string typeOfProcess)
        {
            var selectedYear = typeOfProcess == "Start" ?
                UserStateStorage.userInfo[callbackQuery.From.Id].UserDates.StartYear : UserStateStorage.userInfo[callbackQuery.From.Id].UserDates.EndYear;

            List<InlineKeyboardButton> buttons = new();
            List<string> months = new() { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            if (typeOfProcess == "Start")
            {
                if (DateTime.Now.Year == selectedYear)
                {
                    var currentMonth = DateTime.Now.Month;
                    foreach (var month in months.Select((value, i) => (value, i)))
                    {
                        if (currentMonth <= (month.i + 1))
                        {
                            buttons.Add(new InlineKeyboardButton(month.value) { CallbackData = (month.i + 1).ToString() });
                        }
                    }
                }
                else
                {
                    foreach (var month in months.Select((value, i) => (value, i)))
                    {
                        buttons.Add(new InlineKeyboardButton(month.value) { CallbackData = (month.i + 1).ToString() });
                    }
                }
            }

            if (typeOfProcess == "End")
            {
                if (selectedYear == UserStateStorage.userInfo[callbackQuery.From.Id].UserDates.StartYear)
                {
                    var currentMonth = UserStateStorage.userInfo[callbackQuery.From.Id].UserDates.StartMonth;
                    foreach (var month in months.Select((value, i) => (value, i)))
                    {
                        if (currentMonth <= (month.i + 1))
                        {
                            buttons.Add(new InlineKeyboardButton(month.value) { CallbackData = (month.i + 1).ToString() });
                        }
                    }
                }
                else
                {
                    foreach (var month in months.Select((value, i) => (value, i)))
                    {
                        buttons.Add(new InlineKeyboardButton(month.value) { CallbackData = (month.i + 1).ToString() });
                    }
                }
            }


            buttons.Add(new InlineKeyboardButton($"BACK") { CallbackData = callBackData });

            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, 2);
            return inlineKeyboard;
        }
    }
}
