using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class SendDateYearKeyboard
    {
        public static InlineKeyboardMarkup BuildKeyboard(string callBackData)
        {
            var currentYear = DateTime.Now.Year; 
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
