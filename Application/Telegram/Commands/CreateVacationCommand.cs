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
            var vacationStartDate = Helper.GetStartDate(callbackQuery);
            var vacationEndDate = Helper.GetEndDate(callbackQuery);
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
                    , new List<string>() { "New Booking", "My Bookings", "New Vacation", "My Vacations", "BACK" }
                    , $"You clicked: {callbackQuery.Data} \n Press Button"
                    , 2
                    , "StartingProcess");
                UserStateStorage.UpdateUserState(callbackQuery.From.Id, UserState.SelectingAction);
                return;
            }
            catch
            {
                var message = await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "There were problems, seems you have another vacation in this period or your vacations are crossed! Please, retry again!");
                UserStateStorage.AddMessage(callbackQuery.From.Id, message.MessageId);
                await new SendMonthCommand(_mediator, _bot).SendAsync(callbackQuery, "Please select start date month", "BACKStartingProcess", "Start");
                UserStateStorage.UpdateUserState(callbackQuery.From.Id, UserState.NewVacationIsSelectedStartDateMonth);
                return;
            }

            
        }
    }
}
