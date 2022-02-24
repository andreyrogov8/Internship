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
            await TelegramMessages.Delete(telegraBotClient, update.CallbackQuery.From.Id);
            switch (UserStateStorage.GetUserCurrentState(update.CallbackQuery.From.Id))
            {
                case UserState.StartingProcess:
                    await new ProvideButtons(telegraBotClient).Send(
                        update.CallbackQuery, new List<string>() { "New Booking", "My Bookings", "BACKProcessNotStarted" }, 2);
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.SelectingAction);
                    return;
                case UserState.SelectingAction:
                    {
                        switch (update.CallbackQuery.Data)
                        {
                            case "New Booking":
                                UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.BookingIsSelected);
                                await new ProvideButtons(telegraBotClient).Send(
                                    update.CallbackQuery, new List<string>() { "Next", "BACKStartingProcess" }, 1);

                                return;
                                //case "My Bookings":
                                //    await new SendOfficeListCommand(_mediator, _telegraBotClient, update.Message).Send();
                                //    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.CheckingBookings);
                                //    return;
                        }
                        return;
                    }
                case UserState.BookingIsSelected:
                    await new SendOfficeListCommand(mediator, telegraBotClient).Send(update.CallbackQuery);
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.StartingBooking);
                    return;
                case UserState.StartingBooking:
                    await new SendMapListCommand(mediator, telegraBotClient).Send(update.CallbackQuery);
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.SelectingFloor);
                    return;
                case UserState.SelectingFloor:
                    await new SendWorkplaceListCommand(mediator, telegraBotClient).SendListByMapId(update.CallbackQuery);
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.SelectingWorkplace);
                    return;
                case UserState.SelectingWorkplace:
                    await new SendStartDateMonthCommand(mediator, telegraBotClient).Send(update.CallbackQuery);
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.SelectingStartDateMonth);
                    return;
                case UserState.SelectingStartDateMonth:
                    await new SendStartDateDayCommand(mediator, telegraBotClient).Send(update.CallbackQuery);
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.SelectingStartDateDay);
                    return;
                case UserState.SelectingStartDateDay:
                    await new SendEndDateMonthCommand(mediator, telegraBotClient).Send(update.CallbackQuery);
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.SelectingEndDateMonth);
                    return;
                case UserState.SelectingEndDateMonth:
                    await new SendEndDateDayCommand(mediator, telegraBotClient).Send(update.CallbackQuery);
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.SelectingEndDateDay);
                    return;
                // in the next state => set the day of 'userInfo[telegramId].Booking.EndDate'

            }
        }
    }
}
