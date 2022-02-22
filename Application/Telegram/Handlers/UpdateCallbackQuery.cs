using Application.Telegram.Commands;
using Domain.Enums;
using MediatR;
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
                        update.CallbackQuery, new List<string>() { "New Booking", "My Bookings" }, 2);
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
                            case "My Bookings":
                                await new SendBookingListCommand(mediator, telegraBotClient).SendCurrentUserBookings(update.CallbackQuery);
                                UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.CheckingBookings);
                                return;
                            case string when update.CallbackQuery.Data.Contains("goBack"):
                                await telegraBotClient.SendTextMessageAsync(update.CallbackQuery.From.Id, $"goBack clicked in {UserStateStorage.GetUserCurrentState(update.CallbackQuery.From.Id)}");

                                await new ProvideButtons(telegraBotClient).Send(
                                    update.CallbackQuery, new List<string>() { "New Booking", "My Bookings" }, 2);
                                UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.SelectingAction);
                                return;
                        }
                        return;
                    }
                case UserState.StartingBooking:
                    switch(update.CallbackQuery.Data)
                    {
                        // user decided to go back to main menu from list of maps (floors)
                        case string when update.CallbackQuery.Data.Contains("goBack"):
                            await new ProvideButtons(telegraBotClient).Send(
                        update.CallbackQuery, new List<string>() { "New Booking", "My Bookings" }, 2);
                            UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.SelectingAction);
                            return;
                        default:
                            await telegraBotClient.SendTextMessageAsync(update.CallbackQuery.From.Id, $"goBack clicked in {UserStateStorage.GetUserCurrentState(update.CallbackQuery.From.Id)}");

                            await new SendMapListCommand(mediator, telegraBotClient).Send(update.CallbackQuery);
                            UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.SelectingFloor);
                            return;
            }
                    
                case UserState.SelectingFloor:
                    switch(update.CallbackQuery.Data)
                    {
                        // user decided to go back to list of office list state
                        case string when update.CallbackQuery.Data.Contains("goBack"):
                            await telegraBotClient.SendTextMessageAsync(update.CallbackQuery.From.Id, $"goBack clicked in {UserStateStorage.GetUserCurrentState(update.CallbackQuery.From.Id)}");
                            await new SendOfficeListCommand(mediator, telegraBotClient).Send(update.CallbackQuery);
                            UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.StartingBooking);
                            return;

                        default:
                            await new SendWorkplaceListCommand(mediator, telegraBotClient).SendListByMapId(update.CallbackQuery);
                            UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.SelectingWorkplace);
                            return;
                    }
                case UserState.CheckingBookings:
                    
                    switch(update.CallbackQuery.Data)
                    {
                        // user pressed go Back button to change his state to select menu (new booking, etc...)
                        case string when update.CallbackQuery.Data.Contains("goBack"):
                            await telegraBotClient.SendTextMessageAsync(update.CallbackQuery.From.Id, $"goBack clicked in {UserStateStorage.GetUserCurrentState(update.CallbackQuery.From.Id)}");
                            await new ProvideButtons(telegraBotClient).Send(
                        update.CallbackQuery, new List<string>() { "New Booking", "My Bookings" }, 2);
                            UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.SelectingAction);
                            return;
                        default :
                            return;
                    }
                case UserState.SelectingWorkplace:
                    switch(update.CallbackQuery.Data)
                    {
                        // User decided to quit from selecting workplace, as long as we have no office id we are redirecting to start booking state
                        case string when update.CallbackQuery.Data.Contains("goBack"):
                            await new SendOfficeListCommand(mediator, telegraBotClient).Send(update.CallbackQuery);
                            UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.StartingBooking);
                            return;
                        default:
                            await telegraBotClient.SendTextMessageAsync(update.CallbackQuery.From.Id, $"goBack clicked in {UserStateStorage.GetUserCurrentState(update.CallbackQuery.From.Id)}, default");
                            return;
                    }







            }
        }
    }
}
