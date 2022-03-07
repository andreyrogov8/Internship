using Application.Telegram.Commands;
using Application.Telegram.DataCommands;
using Domain.Enums;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Application.Telegram.MainActions
{
    public class NewVacationCommand
    {


        public static async Task HandleAsync(Update update, TelegramBotClient telegramBotClient, IMediator mediator)
        {
            var user = UserStateStorage.userInfo[update.CallbackQuery.From.Id];

            switch (UserStateStorage.GetUserCurrentState(update.CallbackQuery.From.Id))
            {
                case UserState.NewVacationIsSelected:
                    await new SendMonthCommand(mediator, telegramBotClient).SendAsync(
                        update.CallbackQuery
                        , "Please select start date month"
                        , "BACKStartingProcess"
                        ,"Start");
                    UserStateStorage.UpdateUserState(update.CallbackQuery.From.Id, UserState.NewVacationIsSelectedStartDateMonth);
                    return;
                case UserState.NewVacationIsSelectedStartDateMonth:
                    DateHelper.StartMonthUpdater(update.CallbackQuery, ref user);
                    await new SendDayCommand(mediator, telegramBotClient).SendAsync(
                        update.CallbackQuery
                        , "Please select start date day"
                        , "BACKStartingProcess"
                        ,"Start");
                    UserStateStorage.UpdateUserState(update.CallbackQuery.From.Id, UserState.NewVacationIsSelectedStartDateDay);
                    return;

                case UserState.NewVacationIsSelectedStartDateDay:
                    DateHelper.StartDayUpdater(update.CallbackQuery, ref user);
                    await new SendMonthCommand(mediator, telegramBotClient).SendAsync(
                        update.CallbackQuery
                        , "Please select end date month"
                        , "BACKStartingProcess"
                        ,"End");
                    UserStateStorage.UpdateUserState(update.CallbackQuery.From.Id, UserState.NewVacationIsSelectedEndDateMonth);
                    return;
                case UserState.NewVacationIsSelectedEndDateMonth:
                    DateHelper.EndMonthUpdater(update.CallbackQuery, ref user);
                    await new SendDayCommand(mediator, telegramBotClient).SendAsync(
                        update.CallbackQuery
                        , "Please select end date day"
                        , "BACKStartingProcess"
                        ,"End");

                    UserStateStorage.UpdateUserState(update.CallbackQuery.From.Id, UserState.NewVacationIsSelectedEndDateDay);
                    return;
                case UserState.NewVacationIsSelectedEndDateDay:
                    DateHelper.EndDayUpdater(update.CallbackQuery, ref user);
                    await new CreateVacationCommand(mediator, telegramBotClient).SendAsync(update.CallbackQuery);
                    return;


            }
        }
    }
}
