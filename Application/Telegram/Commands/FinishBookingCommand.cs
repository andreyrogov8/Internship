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
    public class FinishBookingCommand
    {
        public TelegramBotClient _bot;
        public readonly IMediator _mediator;
        public FinishBookingCommand(IMediator mediator, TelegramBotClient bot)
        {
            _bot = bot;
            _mediator = mediator;
        }
        public async Task Execute(CallbackQuery callbackQuery)
        {
            var currentBooking = await _mediator.Send(UserStateStorage.userInfo[callbackQuery.From.Id].Booking);
            
            await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, 
                $"Your booking details: \n From: {currentBooking.StartDate.ToString()} \n" +
                $"To: {currentBooking.EndDate.ToString()} \n " +
                $"Office Name:{currentBooking.OfficeName}, City:{currentBooking.City}, Country:{currentBooking.Country} \n" +
                $"Floor: {currentBooking.FloorNumber}\n" +
                $"Workplace Number:{currentBooking.WorkplaceNumber}");
        }
    }
}
