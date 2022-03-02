﻿using Application.Features.MapFeature.Queries;
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
                    var officeNumber = await mediator.Send(new GetMapByIdQueryRequest() { Id = UserStateStorage.userInfo[update.CallbackQuery.From.Id].MapId });
                    await new SendMonthCommand(mediator, telegraBotClient).SendAsync(update.CallbackQuery, 
                        $"You choose: Floor:{officeNumber.FloorNumber} \n Please select start date month", "BACKNewBookingIsSelected");                    
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.NewBookingIsSelectedSelectingStartDateMonth);
                    return;
                case UserState.NewBookingIsSelectedSelectingStartDateMonth:
                    await new SendDayCommand(mediator, telegraBotClient).SendAsync(update.CallbackQuery, "Please select start date day", "BACKNewBookingIsSelected");
                    DateHelper.StartMonthUpdater(update.CallbackQuery, ref user);
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.NewBookingIsSelectedSelectingStartDateDay);
                    return;
                case UserState.NewBookingIsSelectedSelectingStartDateDay:
                    await new SendMonthCommand(mediator, telegraBotClient).SendAsync(update.CallbackQuery, "Please select end date month", "BACKNewBookingIsSelected");
                    DateHelper.StartDayUpdater(update.CallbackQuery, ref user);
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.NewBookingIsSelectedSelectingEndDateMonth);
                    return;
                case UserState.NewBookingIsSelectedSelectingEndDateMonth:
                    await new SendDayCommand(mediator, telegraBotClient).SendAsync(update.CallbackQuery, "Please select end date day", "BACKNewBookingIsSelected");
                    DateHelper.EndMonthUpdater(update.CallbackQuery, ref user);
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.NewBookingIsSelectedSelectingPcOption);
                    return;

                case UserState.NewBookingIsSelectedSelectingPcOption:
                    DateHelper.EndDayUpdater(update.CallbackQuery, ref user);
                    await new ProvideButtons(telegraBotClient).SendAsync(update.CallbackQuery,new List<string> { "Yes", "No", "BACK" },  "Would you like workplace with PC", 2, "NewBookingIsSelected");
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.NewBookingIsSelectedSelectingMonitorOption);
                    return;

                case UserState.NewBookingIsSelectedSelectingMonitorOption:
                    UserStateStorage.userInfo[update.CallbackQuery.From.Id].HasPc = update.CallbackQuery.Data == "Yes" ? true : false;
                    await new ProvideButtons(telegraBotClient).SendAsync(update.CallbackQuery, new List<string> { "Yes", "No", "BACK" }, "Would you like workplace with Monitor", 2, "NewBookingIsSelected");
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.NewBookingIsSelectedSelectingKeyboardOption);
                    return;

                case UserState.NewBookingIsSelectedSelectingKeyboardOption:
                    UserStateStorage.userInfo[update.CallbackQuery.From.Id].HasMonitor = update.CallbackQuery.Data == "Yes" ? true : false;
                    await new ProvideButtons(telegraBotClient).SendAsync(update.CallbackQuery, new List<string> { "Yes", "No", "BACK" }, "Would you like workplace with Keyboard", 2, "NewBookingIsSelected");
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.NewBookingIsSelectedSelectingMouseOption);
                    return;

                case UserState.NewBookingIsSelectedSelectingMouseOption:
                    UserStateStorage.userInfo[update.CallbackQuery.From.Id].HasKeyboard = update.CallbackQuery.Data == "Yes" ? true : false;
                    await new ProvideButtons(telegraBotClient).SendAsync(update.CallbackQuery, new List<string> { "Yes", "No", "BACK" }, "Would you like workplace with Mouse", 2, "NewBookingIsSelected");
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.NewBookingIsSelectedSelectingHeadseetOption);
                    return;

                case UserState.NewBookingIsSelectedSelectingHeadseetOption:
                    UserStateStorage.userInfo[update.CallbackQuery.From.Id].HasMouse = update.CallbackQuery.Data == "Yes" ? true : false;
                    await new ProvideButtons(telegraBotClient).SendAsync(update.CallbackQuery, new List<string> { "Yes", "No", "BACK" }, "Would you like workplace with Headset", 2, "NewBookingIsSelected");
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.NewBookingIsSelectedSelectingWindowOption);
                    return;

                case UserState.NewBookingIsSelectedSelectingWindowOption:
                    UserStateStorage.userInfo[update.CallbackQuery.From.Id].HasHeadset = update.CallbackQuery.Data == "Yes" ? true : false;
                    await new ProvideButtons(telegraBotClient).SendAsync(update.CallbackQuery, new List<string> { "Yes", "No", "BACK" }, "Would you like workplace near to Window", 2, "NewBookingIsSelected");
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.NewBookingIsSelectedSelectingWorkplace);
                    return;

                case UserState.NewBookingIsSelectedSelectingWorkplace:
                    UserStateStorage.userInfo[update.CallbackQuery.From.Id].HasWindow = update.CallbackQuery.Data == "Yes" ? true : false;
                    await new SendWorkplaceListCommand(mediator, telegraBotClient).SendListByMapIdAsync(update.CallbackQuery);
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.NewBookingIsSelectedFinishingBooking);
                    return;

                case UserState.NewBookingIsSelectedFinishingBooking:
                    UserStateStorage.userInfo[update.CallbackQuery.From.Id].WorkplaceId = Int32.Parse(update.CallbackQuery.Data);                    
                    await new FinishBookingCommand(mediator, telegraBotClient).Execute(update.CallbackQuery);
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.StartingProcess);
                    return;

            }
        }
    }
}
