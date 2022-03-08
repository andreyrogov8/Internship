using MediatR;
using Telegram.Bot;
using Application.Features.BookingFeature.Commands;
using Telegram.Bot.Types;
using Application.Telegram.Keyboards;

namespace Application.Telegram.Commands
{
    public class CancelBookingCommand
    {
        public TelegramBotClient _bot;
        public readonly IMediator _mediator;
        public CancelBookingCommand(IMediator mediator, TelegramBotClient bot)
        {
            _bot = bot;
            _mediator = mediator;
        }

        public async Task CancelBooking(CallbackQuery callbackQuery, string backButtonData)
        {
            await _mediator.Send(new DeleteBookingCommandRequest
            {
                Id = UserStateStorage.userInfo[callbackQuery.From.Id].SelectedBookingId
            });

            var inlineKeyboard = CommandsListKeyboard.BuildKeyboard(new List<string> { "BACK" }, 2, backButtonData);
            var message = await _bot.SendTextMessageAsync(callbackQuery.From.Id, "Booking was successfully cancelled.", replyMarkup: inlineKeyboard);
            UserStateStorage.AddMessage(callbackQuery.From.Id, message.MessageId);
        }
    }
}
