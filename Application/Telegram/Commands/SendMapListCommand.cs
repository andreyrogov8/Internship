using Application.Features.MapFeature.Queries;
using Application.Features.OfficeFeature.Queries;
using Application.Telegram.Keyboards;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Commands
{
    public class SendMapListCommand
    {
        public TelegramBotClient _bot;
        public readonly IMediator _mediator;
        public SendMapListCommand(IMediator mediator, TelegramBotClient bot)
        {
            _bot = bot;
            _mediator = mediator;

        }
        public async Task SendAsync(CallbackQuery callbackQuery)
        {
            var mapResponse = await _mediator.Send(new GetMapListQueryRequest() { OfficeId = callbackQuery.Data});
            var maps = mapResponse.Results;
            var inlineKeyboard = SendMapListKeyboard.BuildKeyboard(maps);
            var office = await _mediator.Send(new GetOfficeByIdQueryRequest() { Id = Int32.Parse(callbackQuery.Data) });   
            var currentMessage = await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id
                , callbackQuery.Data.Contains("BACK") ? " Choose Floor in Office" : $"You choose office: {office.Name} \n Choose Floor in Office"
                , replyMarkup: inlineKeyboard);
            UserStateStorage.AddMessage(callbackQuery.From.Id, currentMessage.MessageId);
        }

    }
}
