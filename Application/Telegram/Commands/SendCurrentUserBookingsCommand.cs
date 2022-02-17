using Application.Features.BookingFeature.Queries;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

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
            var currentUserBookings = await _mediator.Send(new GetBookingListQueryRequest { TelegramId = _message.From.Id.ToString() });
            var bookings = currentUserBookings.Results;
            List<KeyboardButton> buttons = new();
            foreach (var booking in bookings)
            {
                buttons.Add(new KeyboardButton($"Workplace ID : {booking.WorkplaceId}"));
            }
            var replyKeyboard = KeyboardHelper.BuildKeyboard(buttons, 2);
            await _bot.SendTextMessageAsync(_message.Chat.Id, $"@{_message.From.Username}'s booking list: ", replyMarkup: replyKeyboard);
        }
    }
}
