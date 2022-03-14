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
            List<InlineKeyboardButton> buttons = new();
            Dictionary<int, string> monthNames = new Dictionary<int, string>();
            monthNames.Add(1, "January");
            monthNames.Add(2, "February");
            monthNames.Add(3, "March");
            monthNames.Add(4, "April");
            monthNames.Add(5, "May");
            monthNames.Add(6, "June");
            monthNames.Add(7, "July");
            monthNames.Add(8, "August");
            monthNames.Add(9, "September");
            monthNames.Add(10, "October");
            monthNames.Add(11, "November");
            monthNames.Add(12, "December");

            var currentMonth = DateTime.Now.Month;            

            if (typeOfProcess == "Start")
            {
                for (int i = 1; i <= 4; i++)
                {
                    buttons.Add(new InlineKeyboardButton(monthNames[currentMonth]) { CallbackData = currentMonth.ToString() });
                    currentMonth++;
                    if (currentMonth > 12)
                    {
                        currentMonth -= 9;
                    }
                }   
            }

            if (typeOfProcess == "End")
            {
                var startMonth = UserStateStorage.userInfo[callbackQuery.From.Id].UserDates.StartMonth;
                var diff = startMonth - currentMonth >= 0 ? 4-(startMonth - currentMonth) : currentMonth + 4 - startMonth - 12;
                for (int i = 1; i <= diff; i++)
                {
                    buttons.Add(new InlineKeyboardButton(monthNames[startMonth]) { CallbackData = startMonth.ToString() });
                    startMonth++;
                    if (startMonth > 12)
                    {
                        startMonth -= 9;
                    }
                }
            }


            buttons.Add(new InlineKeyboardButton($"BACK") { CallbackData = callBackData });

            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, 2);
            return inlineKeyboard;
        }
    }
}
