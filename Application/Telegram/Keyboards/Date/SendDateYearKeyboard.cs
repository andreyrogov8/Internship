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
            if (typeOfProcess == "End")
            {
                currentYear = UserStateStorage.userInfo[callbackQuery.From.Id].UserDates.StartYear;
            }

            List<InlineKeyboardButton> buttons = new();
            for (int i = currentYear; i < currentYear+5; i++)
            {
                buttons.Add(new InlineKeyboardButton(i.ToString()) { CallbackData = i.ToString()});
            }

            buttons.Add(new InlineKeyboardButton($"BACK") { CallbackData = callBackData });
            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, 2);
            return inlineKeyboard;
        }
    }
}
