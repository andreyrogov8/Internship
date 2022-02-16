﻿using Application.Features.BookingFeature.Queries;
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
            foreach (var booking in bookings)
            {
                cols.Add(new KeyboardButton($"Owner: {booking.UserName}, Work Place ID: {booking.WorkplaceId}"));
                if(Helper())continue;
            }
            await _bot.SendTextMessageAsync(_message.Chat.Id, "Booking List", replyMarkup: _keyboard);
        }
    }
}

//foreach (var booking in bookings)
//{
//    counter++;
//    cols.Add(new KeyboardButton($"Owner: {booking.UserName}, Work Place ID: {booking.WorkplaceId}"));
//    if (counter % 2 != 0) continue;
//    rows.Add(cols.ToArray());
//    cols = new List<KeyboardButton>();
//}


