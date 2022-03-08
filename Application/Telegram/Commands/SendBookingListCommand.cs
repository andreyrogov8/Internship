using Application.Features.BookingFeature.Queries;
using Application.Telegram.Keyboards;
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
        public readonly IMediator _mediator;
        public SendBookingListCommand(IMediator mediator, TelegramBotClient bot)
        {
            _bot = bot;
            _mediator = mediator;
        }
        public async Task SendCurrentUserBookingsAsync(CallbackQuery callbackQuery, string backButtonCallBackData)
        {
            var bookingResponse = await _mediator.Send(new GetBookingListQueryRequest
            {
                TelegramId = callbackQuery.From.Id
            });

            var bookings = bookingResponse.Results;
            var inlineKeyboard = SendBookingListKeyboard.BuildKeyboard(bookings, backButtonCallBackData);
            var currentMessage = await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Booking List", replyMarkup: inlineKeyboard);
            UserStateStorage.AddMessage(callbackQuery.From.Id, currentMessage.MessageId);
        }

    }
}




