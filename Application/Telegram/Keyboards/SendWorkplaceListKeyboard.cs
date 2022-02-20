using Application.Features.CountryCQ;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class SendWorkplaceListKeyboard
    {
        public static ReplyKeyboardMarkup BuildKeyboard(IEnumerable<WorkplaceDto> workplaces)
        {
            var buttons = new List<KeyboardButton>();

            foreach (var workplace in workplaces)
            {
                buttons.Add(new KeyboardButton($"Id: {workplace.Id}, WorkplaceNumber: {workplace.WorkplaceNumber}"));
            }
            var replyKeyboard = KeyboardHelper.BuildKeyboard(buttons, 2);
            return replyKeyboard;
        }
    }
}
