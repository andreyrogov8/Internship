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

namespace Application.Telegram.Commands
{
    public class SendSelectedBookingCommand
    {
        public TelegramBotClient _bot;
        public readonly IMediator _mediator;
        public SendSelectedBookingCommand(IMediator mediator, TelegramBotClient bot)
        {
            _bot = bot;
            _mediator = mediator;
        }

        public async Task SendSelectedBookingAsync(CallbackQuery callbackQuery, string backButtonData)
        {
            var bookingId = Int32.Parse(callbackQuery.Data);
            UserStateStorage.userInfo[callbackQuery.From.Id].SelectedBookingId = bookingId;

            var booking = await _mediator.Send(new GetBookingByIdQueryRequest
            {
                Id = bookingId
            });

            string recurringDay = booking.IsRecurring ? $"Recurring Booking, Reccuring Day: {booking.StartDate.ToString("dddd")}" : "Not Recurring Booking";
            string message = 
                $"Your booking details: \n" +
                $"From: {booking.StartDate.Date.ToShortDateString()} \n" +
                $"To: {booking.EndDate.Date.ToShortDateString()} \n" +
                $"{recurringDay} \n"+
                $"Office Name: {booking.OfficeName} \n" +
                $"City: {booking.City} \n" +
                $"Country: {booking.Country} \n" +
                $"Floor: {booking.FloorNumber} \n" +
                $"Workplace Number: {booking.WorkplaceNumber} \n" +
                $"Next to window: {GetAttributeStatus(booking.HasWindow)} \n" +
                $"Has PC: {GetAttributeStatus(booking.HasPc)} \n" +
                $"Has Monitor: {GetAttributeStatus(booking.HasMonitor)} \n" +
                $"Has Keyboard: {GetAttributeStatus(booking.HasKeyboard)} \n" +
                $"Has Mouse: {GetAttributeStatus(booking.HasMouse)} \n" +
                $"Has Headset: {GetAttributeStatus(booking.HasHeadset)}";

            var inlineKeyboard = CommandsListKeyboard.BuildKeyboard(new List<string> { "Cancel", "BACK" }, 2, backButtonData);
            var currentMessage = await _bot.SendTextMessageAsync(callbackQuery.From.Id, message, replyMarkup: inlineKeyboard);
            UserStateStorage.AddMessage(callbackQuery.From.Id, currentMessage.MessageId);
        }
        private string GetAttributeStatus(bool attribute)
        {
            return attribute ? "Yes" : "No";
        }
    }
}
