using Application.Features.CountriesFeature.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class SendDateMonthKeyboard
    {
        public static InlineKeyboardMarkup BuildKeyboard(string callBackData)
        {
            List<InlineKeyboardButton> buttons = new();
            List<string> months = new() { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            
            foreach (var month in months.Select((value, i) => (value, i)))
            {
                buttons.Add(new InlineKeyboardButton(month.value) { CallbackData = (month.i + 1).ToString() });
            }
            buttons.Add(new InlineKeyboardButton($"BACK") { CallbackData = callBackData });

            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, 2);
            return inlineKeyboard;
        }
    }
}
