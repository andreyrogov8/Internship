using Application.Features.OfficeFeature.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class SendOfficeListKeyboard
    {
        public static InlineKeyboardMarkup BuildKeyboard(IEnumerable<OfficeDto> offices, string stateName="defaultState")
        {
            var buttons = new List<InlineKeyboardButton>();

            foreach (var office in offices)
            {
                buttons.Add(new InlineKeyboardButton($"Name: {office.Name}") { CallbackData = office.Id.ToString() });
            }
            buttons.Add(new InlineKeyboardButton($"◀️Go Back") { CallbackData = $"goBack|{stateName}" });

            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, 2);
            return inlineKeyboard;
        }
    }
}
