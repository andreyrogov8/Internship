using Application.Features.UserFeature.Queries;
using Application.Features.VacationFeature.Commands;
using Domain.Enums;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Application.Telegram.Commands
{
    public class CreateVacationCommand
    {
        private readonly IMediator _mediator;
        private readonly TelegramBotClient _bot;

        public CreateVacationCommand(IMediator mediator, TelegramBotClient bot)
        {
            _mediator = mediator;
            _bot = bot;

        }
        public async Task SendAsync(CallbackQuery callbackQuery)
        {
            var userInputs = UserStateStorage.userInfo[callbackQuery.From.Id].UserDates;
            var vacationStartDate = new DateTimeOffset(
                DateTimeOffset.UtcNow.Year,
                month: userInputs.StartMonth,
                day: userInputs.StartDay,
                0, 0, 0,
                TimeSpan.Zero);
            var vacationEndDate = new DateTimeOffset(
                DateTimeOffset.UtcNow.Year,
                month: userInputs.EndMonth,
                day: userInputs.EndDay,
                0, 0, 0,
                TimeSpan.Zero);
            try
            {
                var user = await _mediator.Send(new GetUserByIdQueryRequest { TelegramId = callbackQuery.From.Id });

                var response = await _mediator.Send(new CreateVacationCommandRequest
                {
                    UserId = user.Id,
                    VacationStart = vacationStartDate,
                    VacationEnd = vacationEndDate
                });
                var message = await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "You have successfully created new vacation for you!");
                UserStateStorage.AddMessage(callbackQuery.From.Id, message.MessageId);
                await new ProvideButtons(_bot).SendAsync(
                    callbackQuery
                    , new List<string>() { "New Booking", "My Bookings", "New Vacation", "BACK" }
                    , $"You clicked: {callbackQuery.Data} \n Press Button"
                    , 2
                    , "ProcessNotStarted");
                UserStateStorage.UserStateUpdate(callbackQuery.From.Id, UserState.SelectingAction);
                return;
            }
            catch
            {
                var message = await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "There were problems, seems you have another vacation in this period or your vacations are crossed! Please, retry again!");
                UserStateStorage.AddMessage(callbackQuery.From.Id, message.MessageId);
                await new SendMonthCommand(_mediator, _bot).SendAsync(callbackQuery, "Please select start date month", "BACKStartingProcess");
                UserStateStorage.UserStateUpdate(callbackQuery.From.Id, UserState.NewVacationIsSelectedStartDateMonth);
                return;
            }

            
        }
    }
}
