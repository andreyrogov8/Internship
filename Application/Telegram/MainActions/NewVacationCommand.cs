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
                    await new SendMonthCommand(mediator, telegramBotClient).SendAsync(update.CallbackQuery, "Please select start date month", "BACKStartingProcess");
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.NewVacationIsSelectedStartDateMonth);
                    return;
                case UserState.NewVacationIsSelectedStartDateMonth:
                    DateHelper.VacationStartMonthUpdater(update.CallbackQuery, ref user);
                    await new SendDayCommand(mediator, telegramBotClient).Sendasync(update.CallbackQuery, "Please select start date day", "BACKStartingProcess");
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.NewVacationIsSelectedStartDateDay);
                    return;

                case UserState.NewVacationIsSelectedStartDateDay:
                    DateHelper.VacationStartDayUpdater(update.CallbackQuery, ref user);
                    await new SendMonthCommand(mediator, telegramBotClient).SendAsync(update.CallbackQuery, "Please select end date month", "BACKStartingProcess");
                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.NewVacationIsSelectedEndDateMonth);
                    return;
                case UserState.NewVacationIsSelectedEndDateMonth:
                    DateHelper.VacationEndMonthUpdater(update.CallbackQuery, ref user);
                    await new SendDayCommand(mediator, telegramBotClient).Sendasync(update.CallbackQuery, "Please select end date day", "BACKStartingProcess");

                    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.NewVacationIsSelectedEndDateDay);
                    return;
                case UserState.NewVacationIsSelectedEndDateDay:
                    DateHelper.VacationEndDayUpdater(update.CallbackQuery, ref user);
                    await new CreateVacationCommand(mediator, telegramBotClient).SendAsync(update.CallbackQuery);
                    return;


            }
        }
    }
}
