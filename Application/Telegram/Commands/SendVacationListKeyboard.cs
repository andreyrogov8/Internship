using Application.Features.BookingFeature.Queries;
using Application.Features.VacationFeature.Queries;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class SendVacationListKeyboard
    {
        public static InlineKeyboardMarkup BuildKeyboard(IEnumerable<VacationDTO> vacations)
        {
            List<InlineKeyboardButton> buttons = new();
            foreach (var vacation in vacations)
            {
                buttons.Add(new InlineKeyboardButton($"Start: {vacation.VacationStart.Year}|{vacation.VacationStart.Month}|{vacation.VacationStart.Day}, End: {vacation.VacationEnd.Year}|{vacation.VacationEnd.Month}|{vacation.VacationEnd.Day}") { CallbackData = $"{vacation.Id}" });
            }
            buttons.Add(new InlineKeyboardButton("BACK") { CallbackData = "BACKStartingProcess" });

            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, 2);
            return inlineKeyboard;
        }
    }
}
