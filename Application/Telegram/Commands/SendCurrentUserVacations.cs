using Application.Features.VacationFeature.Queries;
using Application.Telegram.Keyboards;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Application.Telegram.Commands
{
    public class SendCurrentUserVacations
    {
        private readonly IMediator _mediator;
        private readonly TelegramBotClient _bot;

        public SendCurrentUserVacations(IMediator mediator, TelegramBotClient bot)
        {
            _mediator = mediator;
            _bot = bot;
        }

        public string FormatVacationList(IEnumerable<VacationDTO> vacations)
        {
            StringBuilder result = new StringBuilder();
            int counter = 1;
            foreach(var vacation in vacations)
            {
                var startDate = vacation.VacationStart;
                var endDate = vacation.VacationEnd;
                string vacationFormat = $"{counter}) Starts - Year: {startDate.Year} | Month: {startDate.Month} | Day: {startDate.Day}\n    Ends - Year: {endDate.Year} | Month: {endDate.Month} | Day: {endDate.Day}\n\n";
                counter++;
                result.Append(vacationFormat);
            }
            return result.ToString();
        }

        public async Task SendAsync(CallbackQuery callbackQuery)
        {
            var vacationResponse = await _mediator.Send(new GetVacationListQueryRequest
            {
                TelegramId = callbackQuery.From.Id
            });
            var vacations = vacationResponse.Results;
            var text = FormatVacationList(vacations);
            var inlineKeyboard = SendVacationListKeyboard.BuildKeyboard(vacations);
            var currentMessage = await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"Your vacations:\n\n{text}", replyMarkup: inlineKeyboard);
            UserStateStorage.AddMessage(callbackQuery.From.Id, currentMessage.MessageId);

        }
    }
}
