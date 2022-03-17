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
                        StartDate = Helper.GetStartDate(callbackQuery),
                        EndDate = Helper.GetEndDate(callbackQuery),
                        IsRecurring = currentUserInfo.RecurringDay is not null,
                        Frequency = 0
                    });
            string recurringDayInfo = (currentUserInfo.RecurringDay is not null) ? $"Recurring Day: {currentUserInfo.RecurringDay}" : "Not Recurring Booking";
            var inlineKeyboard = CommandsListKeyboard.BuildKeyboard(new List<string>{ "Start New"}, 1, "BACKStartingProcess");
            var currentMessage = await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, 
                $"Your booking details: \n From: {currentBooking.StartDate.Date.ToShortDateString()} " +
                $"To: {currentBooking.EndDate.Date.ToShortDateString()} \n " +
                $"{recurringDayInfo}  \n" +
                $"Office Name:{currentBooking.OfficeName}, City:{currentBooking.City}, Country:{currentBooking.Country} \n" +
                $"Floor: {currentBooking.FloorNumber}, Workplace Number:{currentBooking.WorkplaceNumber} \n" +
                $"Next to window: {GetAttributeStatus(currentBooking.HasWindow)} \n" +
                $"Has PC: {GetAttributeStatus(currentBooking.HasPc)} \n" +
                $"Has Monitor: {GetAttributeStatus(currentBooking.HasMonitor)} \n" +
                $"Has Keyboard: {GetAttributeStatus(currentBooking.HasKeyboard)} \n" +
                $"Has Mouse: {GetAttributeStatus(currentBooking.HasMouse)} \n" +
                $"Has Headset: {GetAttributeStatus(currentBooking.HasHeadset)}"                
                , replyMarkup:inlineKeyboard);
            UserStateStorage.AddMessage(callbackQuery.From.Id, currentMessage.MessageId);
            UserStateStorage.Clear(callbackQuery.From.Id);
        }

        private string GetAttributeStatus(bool attribute) 
        {
            return attribute ? "Yes" : "No";
        }
    }
}
