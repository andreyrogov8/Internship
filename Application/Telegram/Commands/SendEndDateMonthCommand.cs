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
    public class SendEndDateMonthCommand
    {
        public TelegramBotClient _bot;
        public readonly IMediator _mediator;
        public SendEndDateMonthCommand(IMediator mediator, TelegramBotClient bot)
        {
            _bot = bot;
            _mediator = mediator;
        }

        public async Task Send(CallbackQuery callbackQuery)
        {
            int day = Int32.Parse(callbackQuery.Data);
            var inlineKeyboard = SendDateMonthKeyboard.BuildKeyboard();
            var currentMessage = await _bot.SendTextMessageAsync(
                callbackQuery.Message.Chat.Id,
                "Please select end date month",
                replyMarkup: inlineKeyboard);

            var booking = UserStateStorage.userInfo[callbackQuery.From.Id].Booking;
            var currentStartDate = booking.StartDate;
            Console.WriteLine($"<<<<<<<<<<<< Start Date DAY: {day}, Start Date MONTH: {currentStartDate.Month} >>>>>>>>>>>>>>");
            booking.StartDate = new DateTimeOffset(
                DateTimeOffset.UtcNow.Year,
                currentStartDate.Month,
                day,
                0, 0, 0,
                currentStartDate.Offset);

            UserStateStorage.AddMessage(callbackQuery.From.Id, currentMessage.MessageId);
        }
    }
}
