using Application.Features.VacationFeature.Commands;
using Application.Features.VacationFeature.Queries;
using Application.Telegram.Keyboards;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Application.Telegram.Commands
{
    public class VacationDetail
    {
        private readonly IMediator _mediator;
        private readonly TelegramBotClient _bot;

        public VacationDetail(IMediator mediator, TelegramBotClient bot)
        {
            _mediator = mediator;
            _bot = bot;

        }
        public async Task SendAsync(CallbackQuery callbackQuery)
        {
            var vacationId = int.Parse(callbackQuery.Data);
            var vacation = await _mediator.Send(new GetVacationByIdQueryRequest { Id = vacationId });
            var text = $"Starts - Year: {vacation.VacationStart.Year} | Month: {vacation.VacationStart.Month} | Day: {vacation.VacationStart.Day}\nEnds - Year: {vacation.VacationEnd.Year} | Month: {vacation.VacationEnd.Month} | Day: {vacation.VacationEnd.Day}\n\n";

            var inlineKeyboard = DetailsKeyboard.BuildKeyboard(vacation);
            var currentMessage = await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, text, replyMarkup: inlineKeyboard);
            UserStateStorage.AddMessage(callbackQuery.From.Id, currentMessage.MessageId);
        }
        public async Task DeleteAsync(CallbackQuery callbackQuery)
        {
            var vacationId = int.Parse(callbackQuery.Data.Split('|')[2]);
            var response = await _mediator.Send(new DeleteVacationCommandRequest
            {
                Id = vacationId
            });
            var text = $"You have successfully deleted your vacation!";
            var inlineKeyboard = SendVacationListKeyboard.SendBackButton();
            var currentMessage = await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, text, replyMarkup: inlineKeyboard);
            UserStateStorage.AddMessage(callbackQuery.From.Id, currentMessage.MessageId);

        }

    }
}
