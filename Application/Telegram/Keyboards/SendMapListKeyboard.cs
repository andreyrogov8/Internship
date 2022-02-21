using Application.Features.MapFeature.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class SendMapListKeyboard
    {
        public static InlineKeyboardMarkup BuildKeyboard(IEnumerable<MapDTO> maps, string stateName="defaultState")
        {
            var buttons = new List<InlineKeyboardButton>();

            foreach (var map in maps)
            {
                buttons.Add(new InlineKeyboardButton($"Floor: {map.FloorNumber}") { CallbackData = map.OfficeId.ToString() });
            }
            buttons.Add(new InlineKeyboardButton($"◀️Go Back") { CallbackData = $"goBack|{stateName}" });

            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, 2);
            return inlineKeyboard;
        }
    }
}
