using Application.Features.BookingFeature.Queries;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class SendCurrentUserBookingsListKeyboard
    {
        public static ReplyKeyboardMarkup BuildKeyboard(List<BookingDto> bookings)
        {
            List<KeyboardButton> buttons = new();
            foreach (var booking in bookings)
            {
                buttons.Add(new KeyboardButton($"Workplace ID : {booking.WorkplaceId}"));
            }
            var replyKeyboard = KeyboardHelper.BuildKeyboard(buttons, 2);
            return replyKeyboard;
        }
    }
}
