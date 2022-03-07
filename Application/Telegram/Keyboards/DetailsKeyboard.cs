using Application.Features.VacationFeature.Queries;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public class DetailsKeyboard
    {
        public static InlineKeyboardMarkup BuildKeyboard(GetVacationByIdQueryResponse vacation)
        {
            List<InlineKeyboardButton> buttons = new();
            buttons.Add(new InlineKeyboardButton("Delete") { CallbackData = $"Delete|vacation|{vacation.Id}" });
            buttons.Add(new InlineKeyboardButton("BACK") { CallbackData = "BACKStartingProcess" });
            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, 2);

            return inlineKeyboard;
        }
    }
}
