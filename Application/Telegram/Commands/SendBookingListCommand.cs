using Application.Features.BookingFeature.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Commands
{
    public class SendBookingListCommand
    {
        public TelegramBotClient _bot;
        public Message _message;
        public readonly IMediator _mediator;
        public SendBookingListCommand(IMediator mediator, TelegramBotClient bot, Message message)
        {
            _bot = bot;
            _message = message;
            _mediator = mediator;
        }
        public async Task Send()
        {
            var bookingResponse = await _mediator.Send(new GetBookingListQueryRequest());
            var bookings = bookingResponse.Results;
            List<KeyboardButton> buttons = new();
            foreach (var booking in bookings)
            {
                buttons.Add(new KeyboardButton($"Owner: {booking.UserName}, Work Place ID: {booking.WorkplaceId}"));
            }
            var replyKeyboard = KeyboardHelper.BuildKeyboard(buttons, 2);
            await _bot.SendTextMessageAsync(_message.Chat.Id, "Booking List", replyMarkup: replyKeyboard);
        }
    }
}




