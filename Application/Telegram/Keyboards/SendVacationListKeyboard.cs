using Application.Features.VacationFeature.Queries;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class SendVacationListKeyboard
    {
        public static InlineKeyboardMarkup SendBackButton()
        {
            List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>();
            buttons.Add(new InlineKeyboardButton("BACK TO HOME") { CallbackData = "BACKStartingProcess" });
            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, 1);
            return inlineKeyboard;
        }


        public static InlineKeyboardMarkup BuildKeyboard(IEnumerable<VacationDTO> vacations)
        {
            int counter = 0;
            List<InlineKeyboardButton> buttons = new();
            foreach(var vacation in vacations)
            {
                counter++;
                buttons.Add(new InlineKeyboardButton($"{counter}") { CallbackData = $"{vacation.Id}" });
            }
            buttons.Add(new InlineKeyboardButton("BACK") { CallbackData = "BACKStartingProcess" });

            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, 2);
            return inlineKeyboard;
        }
    }
}
