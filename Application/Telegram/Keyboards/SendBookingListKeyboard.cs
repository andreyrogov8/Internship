﻿using Application.Features.BookingFeature.Queries;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class SendBookingListKeyboard
    {
        public static InlineKeyboardMarkup BuildKeyboard(List<BookingDto> bookings, string backButtonCallBackData)
        {
            List<InlineKeyboardButton> buttons = new();
            foreach (var booking in bookings)
            {
                buttons.Add(new InlineKeyboardButton($"{booking.OfficeName} - {booking.WorkplaceNumber}") { CallbackData = booking.Id.ToString() });
            }
            buttons.Add(new InlineKeyboardButton("BACK") { CallbackData = $"BACK{backButtonCallBackData}" });

            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, 2);
            return inlineKeyboard;
        }
    }
}
