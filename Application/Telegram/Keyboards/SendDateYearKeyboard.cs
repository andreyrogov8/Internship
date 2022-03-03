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
            List<InlineKeyboardButton> buttons = new();
            for (int i = 2022; i < 2027; i++)
            {
                buttons.Add(new InlineKeyboardButton(i.ToString()) { CallbackData = i.ToString()});
            }

            buttons.Add(new InlineKeyboardButton($"BACK") { CallbackData = callBackData });
            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, 2);
            return inlineKeyboard;
        }
    }
}
