using Application.Telegram.Commands;
using Application.Telegram.DataCommands;
using Domain.Enums;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Application.Telegram.MainActions
{
    public static class NewBookingCommand
    {
        public static async Task HandleAsync(Update update, TelegramBotClient telegraBotClient, IMediator mediator)
        {
            var user = UserStateStorage.userInfo[update.CallbackQuery.From.Id];

            switch (UserStateStorage.GetUserCurrentState(update.CallbackQuery.From.Id))
            {
                case UserState.NewBookingIsSelected:
                    await new SendOfficeListCommand(mediator, telegraBotClient).SendAsync(update.CallbackQuery);
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.NewBookingIsSelectedStartingBooking);
                    return;
                case UserState.NewBookingIsSelectedStartingBooking:
                    await new SendMapListCommand(mediator, telegraBotClient).SendAsync(update.CallbackQuery);
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.NewBookingIsSelectedSelectingFloor);
                    return;
                case UserState.NewBookingIsSelectedSelectingFloor:
                    UserStateStorage.userInfo[update.CallbackQuery.From.Id].MapId = Int32.Parse(update.CallbackQuery.Data);
                    await new SendMonthCommand(mediator, telegraBotClient).SendAsync(update.CallbackQuery, "Please select start date month", "BACKNewBookingIsSelected");
                    UserStateStorage.userInfo[update.CallbackQuery.From.Id].WorkPlaceId = Int32.Parse(update.CallbackQuery.Data);
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.NewBookingIsSelectedSelectingStartDateMonth);
                    return;
                case UserState.NewBookingIsSelectedSelectingStartDateMonth:
                    await new SendDayCommand(mediator, telegraBotClient).Sendasync(update.CallbackQuery, "Please select start date day", "BACKNewBookingIsSelected");
                    DateHelper.StartMonthUpdater(update.CallbackQuery, ref user);
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.NewBookingIsSelectedSelectingStartDateDay);
                    return;
                case UserState.NewBookingIsSelectedSelectingStartDateDay:
                    await new SendMonthCommand(mediator, telegraBotClient).Send(update.CallbackQuery, "Please select end date month", "BACKNewBookingIsSelected");
                    DateHelper.BookingStartDayUpdater(update.CallbackQuery);
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.NewBookingIsSelectedSelectingEndDateMonth);
                    return;
                case UserState.NewBookingIsSelectedSelectingEndDateMonth:
                    await new SendDayCommand(mediator, telegraBotClient).Send(update.CallbackQuery, "Please select end date day", "BACKNewBookingIsSelected");
                    DateHelper.BookingEndMonthUpdater(update.CallbackQuery);
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.NewBookingIsSelectedSelectingWorkplace);
                    return;

                case UserState.NewBookingIsSelectedSelectingWorkplace:
                    DateHelper.BookingEndDayUpdater(update.CallbackQuery);
                    await new SendWorkplaceListCommand(mediator, telegraBotClient).SendListByMapId(update.CallbackQuery);
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.NewBookingIsSelectedSelectingWorkplace);
                    return;

                // in the next state => set the day of 'userInfo[telegramId].Booking.EndDate'
                case UserState.EnteringVacation:
                    return;
            }
        }
    }
}
