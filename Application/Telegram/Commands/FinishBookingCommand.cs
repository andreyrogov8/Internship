using Application.Features.BookingFeature.Commands;
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
            var currentUserInfo = UserStateStorage.userInfo[callbackQuery.From.Id];
            var currentBooking = await _mediator.Send(
                new CreateBookingCommandRequest()
                    {
                        UserId = currentUserInfo.UserId,
                        WorkplaceId = currentUserInfo.WorkplaceId,
                        StartDate = new DateTimeOffset(
                                            DateTimeOffset.UtcNow.Year,
                                            month: currentUserInfo.UserDates.StartMonth,
                                            day: currentUserInfo.UserDates.StartDay,
                                            0, 0, 0,
                                            TimeSpan.Zero),
                        EndDate = new DateTimeOffset(
                                            DateTimeOffset.UtcNow.Year,
                                            month: currentUserInfo.UserDates.EndMonth,
                                            day: currentUserInfo.UserDates.EndDay,
                                            0, 0, 0,
                                            TimeSpan.Zero),
                        IsRecurring = false,
                        Frequency = 10

                    });
            await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, 
                $"Your booking details: \n From: {currentBooking.StartDate.Date.ToShortDateString()} " +
                $"To: {currentBooking.EndDate.Date.ToShortDateString()} \n " +
                $"Office Name:{currentBooking.OfficeName}, City:{currentBooking.City}, Country:{currentBooking.Country} \n" +
                $"Floor: {currentBooking.FloorNumber}\n" +
                $"Workplace Number:{currentBooking.WorkplaceNumber}");
        }
    }
}
