using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class SendDateYearKeyboard
    {
        public static InlineKeyboardMarkup BuildKeyboard(CallbackQuery callbackQuery, string callBackData, string typeOfProcess)
        {
            var currentYear = DateTime.Now.Year;
            var endYear = currentYear;
            if (typeOfProcess == "End")
            {                
                var startDate = Helper.GetStartDate(callbackQuery);
                var endDate = startDate.AddMonths(3);
                endYear = endDate.Year;
            }

            List<InlineKeyboardButton> buttons = new();
            for (int i = currentYear; i <= endYear; i++)
            {
                buttons.Add(new InlineKeyboardButton(i.ToString()) { CallbackData = i.ToString()});
            }

            buttons.Add(new InlineKeyboardButton($"BACK") { CallbackData = callBackData });
            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, 2);
            return inlineKeyboard;
        }
    }
}
