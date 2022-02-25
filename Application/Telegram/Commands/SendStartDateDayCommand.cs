using Application.Features.CountriesFeature.Queries;
using Application.Telegram.Keyboards;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Application.Telegram.Commands
{
    public class SendStartDateDayCommand
    {
        public TelegramBotClient _bot;
        public readonly IMediator _mediator;
        public SendStartDateDayCommand(IMediator mediator, TelegramBotClient bot)
        {
            _bot = bot;
            _mediator = mediator;

        }

        public async Task Send(CallbackQuery callbackQuery)
        {
            int month = Int32.Parse(callbackQuery.Data);
            var inlineKeyboard = SendDateDayKeyboard.BuildKeyboard(month);
            var currentMessage = await _bot.SendTextMessageAsync(
                callbackQuery.Message.Chat.Id,
                "Please select start date day",
                replyMarkup: inlineKeyboard);

            var booking = UserStateStorage.userInfo[callbackQuery.From.Id].Booking;
            var currentStartDate = booking.StartDate;
            booking.StartDate = new DateTimeOffset(
                DateTimeOffset.UtcNow.Year,
                month, 
                currentStartDate.Day,
                0, 0, 0, 
                currentStartDate.Offset);

            UserStateStorage.AddMessage(callbackQuery.From.Id, currentMessage.MessageId);
        }
    }
}
