using Application.Features.BookingFeature.Queries;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class SendBookingListKeyboard
    {
        public static InlineKeyboardMarkup BuildKeyboard(List<BookingDto> bookings)
        {
            List<InlineKeyboardButton> buttons = new();
            foreach (var booking in bookings)
            {
                buttons.Add(new InlineKeyboardButton($"Owner: {booking.UserName}") { CallbackData = booking.WorkplaceId.ToString() });
            }
            buttons.AddBackButton();
            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, 2);
            return inlineKeyboard;
        }
    }
}
