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
    public class SendEndDateDayCommand
    {
        public TelegramBotClient _bot;
        public readonly IMediator _mediator;
        public SendEndDateDayCommand(IMediator mediator, TelegramBotClient bot)
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
                "Please select end date day",
                replyMarkup: inlineKeyboard);

            var booking = UserStateStorage.userInfo[callbackQuery.From.Id].Booking;
            var currentEndDate = booking.EndDate;
            Console.WriteLine($"<<<<<<<<<<< END DATE MoNTH: {month},  >>>>>>>>>>>>>");
            booking.EndDate = new DateTimeOffset(
                DateTimeOffset.UtcNow.Year,
                month,
                currentEndDate.Day,
                0, 0, 0,
                currentEndDate.Offset);
            Console.WriteLine($"Start Date: {booking.StartDate}, End Date: {booking.EndDate}");
            UserStateStorage.AddMessage(callbackQuery.From.Id, currentMessage.MessageId);
        }
    }
}
