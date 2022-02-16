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
    public class SendBookingListCommand : CommandBase
    {
        public SendBookingListCommand(IMediator mediator, TelegramBotClient bot, Message message) : base(mediator, bot, message)
        {
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
            var replyKeyboard = BuildKeyboard(buttons, 2);
            await _bot.SendTextMessageAsync(_message.Chat.Id, "Booking List", replyMarkup: replyKeyboard);
        }
    }
}




