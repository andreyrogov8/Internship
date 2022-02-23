using Application.Telegram.Commands;
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
            switch (UserStateStorage.GetUserCurrentState(update.CallbackQuery.From.Id))
            {
                case UserState.StartingProcess:
                    await new ProvideButtons(telegraBotClient).Send(
                        update.CallbackQuery, new List<string>() { "New Booking", "My Bookings", "New Vacation"}, 2);
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.SelectingAction);
                    return;
                case UserState.SelectingAction:
                    {
                        switch (update.CallbackQuery.Data)
                        {
                            case "New Booking":
                                await new SendOfficeListCommand(mediator, telegraBotClient).Send(update.CallbackQuery);
                                UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.StartingBooking);
                                return;
                            case "New Vacation":
                                await new CreateVacationCommand(mediator, telegraBotClient).Send(update.CallbackQuery);
                                UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.EnteringVacation);
                                return;
                                //case "My Bookings":
                                //    await new SendOfficeListCommand(_mediator, _telegraBotClient, update.Message).Send();
                                //    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.CheckingBookings);
                                //    return;
                        }
                        return;
                    }
                case UserState.StartingBooking:
                    await new SendMapListCommand(mediator, telegraBotClient).Send(update.CallbackQuery);
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.SelectingFloor);
                    return;
                case UserState.SelectingFloor:
                    await new SendWorkplaceListCommand(mediator, telegraBotClient).SendListByMapId(update.CallbackQuery);
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.SelectingWorkplace);
                    return;
                case UserState.EnteringVacation:

                    return;

            }
        }
    }
}
