using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class SendDateDayKeyboard
    {
        public static InlineKeyboardMarkup BuildKeyboard(int month)
        {
            List<InlineKeyboardButton> buttons = new();
            int numberOfDays = DateTime.DaysInMonth(DateTimeOffset.UtcNow.Year, month);
            for (int day = 1; day <= numberOfDays; day++)
            {
                buttons.Add(new InlineKeyboardButton(day.ToString()) { CallbackData = day.ToString() });
            }
            buttons.Add(new InlineKeyboardButton($"BACK") { CallbackData = "BACKBookingIsSelected" });
            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, 7);
            return inlineKeyboard;
        }
    }
}
