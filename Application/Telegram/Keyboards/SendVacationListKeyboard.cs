using Application.Features.VacationFeature.Queries;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class SendVacationListKeyboard
    {
        public static InlineKeyboardMarkup BuildKeyboard(IEnumerable<VacationDTO> vacations)
        {
            List<InlineKeyboardButton> buttons = new();
            buttons.Add(new InlineKeyboardButton("BACK") { CallbackData = "BACKStartingProcess" });

            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, 2);
            return inlineKeyboard;
        }
    }
}
