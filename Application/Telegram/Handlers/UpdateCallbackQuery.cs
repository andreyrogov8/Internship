using Application.Telegram.Commands;
using Application.Telegram.DataCommands;
using Application.Telegram.MainActions;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Application.Telegram.Handlers
{
    public static class UpdateCallbackQuery
    {
        public static async Task Handle(Update update, TelegramBotClient telegraBotClient, IMediator mediator)
        {
            await Helper.DeleteMessageAsync(telegraBotClient, update.CallbackQuery.From.Id);
            var userState = UserStateStorage.GetUserCurrentState(update.CallbackQuery.From.Id);
            if (userState == UserState.StartingProcess)
            {
                await new ProvideButtons(telegraBotClient).SendAsync(
                update.CallbackQuery, new List<string>() { "New Booking", "My Bookings", "New Vacation", "My Vacations", "BACKProcessNotStarted" }, 2);
                UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.SelectingAction);
                return;
            }
            if (userState == UserState.SelectingAction)
            {
                switch (update.CallbackQuery.Data)
                {
                    case "New Booking":
                        UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.NewBookingIsSelected);
                        await new ProvideButtons(telegraBotClient).SendAsync(
                            update.CallbackQuery, new List<string>() { "Next", "BACKStartingProcess" }, 1);

                        return;
                    case "New Vacation":
                        UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.NewVacationIsSelected);
                        await new ProvideButtons(telegraBotClient).SendAsync(
                            update.CallbackQuery, new List<string>() { "Next", "BACKStartingProcess" }, 1);
                        return;
                    case "My Bookings":
                        await new SendBookingListCommand(mediator, telegraBotClient).SendCurrentUserBookingsAsync(update.CallbackQuery);
                        UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.CheckingBookings);
                        return;
                    case "My Vacations":
                        await new SendCurrentUserVacations(mediator, telegraBotClient).SendAsync(update.CallbackQuery);
                        UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.CheckingVacations);
                        return;

                }
                return;
            }
            if (userState.ToString().Contains("NewBookingIsSelected"))
            {
                await NewBookingCommand.HandleAsync(update, telegraBotClient, mediator);
            }
            if (userState.ToString().Contains("NewVacationIsSelected"))
            {
                await NewVacationCommand.HandleAsync(update, telegraBotClient, mediator);
            }
        }
    }
}
