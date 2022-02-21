using Application.Features.CountryCQ;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class SendWorkplaceListKeyboard
    {
        public static InlineKeyboardMarkup BuildKeyboard(IEnumerable<WorkplaceDto> workplaces, string stateName="defaultState")
        {
            var buttons = new List<InlineKeyboardButton>();

            foreach (var workplace in workplaces)
            {
                buttons.Add(new InlineKeyboardButton($"WorkplaceNumber: {workplace.WorkplaceNumber}") { CallbackData = workplace.Id.ToString()});
            }
            buttons.Add(new InlineKeyboardButton($"◀️Go Back") { CallbackData = $"goBack|{stateName}" });

            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, 2);
            return inlineKeyboard;
        }
    }
}
