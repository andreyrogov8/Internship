using Application.Features.BookingFeature.Queries;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class SendBookingListKeyboard
    {
        public static ReplyKeyboardMarkup BuildKeyboard(List<BookingDto> bookings)
        {
            List<KeyboardButton> buttons = new();
            foreach (var booking in bookings)
            {
                buttons.Add(new KeyboardButton($"Owner: {booking.UserName}, Work Place ID: {booking.WorkplaceId}"));
            }
            var replyKeyboard = KeyboardHelper.BuildKeyboard(buttons, 2);
            return replyKeyboard;
        }
    }
}
