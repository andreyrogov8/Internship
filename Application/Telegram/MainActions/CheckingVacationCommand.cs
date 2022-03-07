using Application.Telegram.Commands;
using Domain.Enums;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Application.Telegram.MainActions
{
    public static class CheckingVacationCommand
    {
        public static async Task HandleAsync(Update update, TelegramBotClient telegraBotClient, IMediator mediator)
        {
            var user = UserStateStorage.userInfo[update.CallbackQuery.From.Id];

            switch (UserStateStorage.GetUserCurrentState(update.CallbackQuery.From.Id))
            {
                case UserState.CheckingVacations:
                    await new VacationDetail(mediator, telegraBotClient).SendAsync(update.CallbackQuery);
                    UserStateStorage.UpdateUserState(update.CallbackQuery.From.Id, UserState.CheckingVacationsDetails);
                    return;
                case UserState.CheckingVacationsDetails:
                    await new VacationDetail(mediator, telegraBotClient).DeleteAsync(update.CallbackQuery);
                    return;
            }
        }
    }
}
