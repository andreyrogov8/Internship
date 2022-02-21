using Application.Features.BookingFeature.Queries;
using Application.Telegram.Keyboards;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Application.Telegram.Commands
{
    public class SendCurrentUserBookingsCommand 
    {
        public TelegramBotClient _bot;
        public Message _message;
        public readonly IMediator _mediator;
        public SendCurrentUserBookingsCommand(IMediator mediator, TelegramBotClient bot, Message message)
        {
            _bot = bot;
            _message = message;
            _mediator = mediator;
        }

        
        public async Task Send()
        {
            var currentUserBookings = await _mediator.Send(new GetBookingListQueryRequest { TelegramId = _message.From.Id });
            var bookings = currentUserBookings.Results;
            var replyKeyboard = SendCurrentUserBookingsListKeyboard.BuildKeyboard(bookings);
            await _bot.SendTextMessageAsync(_message.Chat.Id, $"@{_message.From.Username}'s booking list: ", replyMarkup: replyKeyboard);
        }
    }
}
