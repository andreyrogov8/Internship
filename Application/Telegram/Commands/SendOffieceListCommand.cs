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
    public class SendOfficeListCommand
    {    
        public TelegramBotClient _bot;
        public readonly IMediator _mediator;
        public SendOfficeListCommand(IMediator mediator, TelegramBotClient bot)
        {
            _bot = bot;
            _mediator = mediator;

        }
        public async Task SendAsync(CallbackQuery callbackQuery)
        {
            var officeResponse = await _mediator.Send(new GetOfficeListQueryRequest());
            var offices = officeResponse.Results;
            var inlineKeyboard = SendOfficeListKeyboard.BuildKeyboard(offices);
            var currnetMessage = await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id,
               callbackQuery.Data.Contains("BACK") ? "Choose Office or write for filtering" : $"You choose: {callbackQuery.Data} \n Choose Office or write for filtering"
               ,replyMarkup: inlineKeyboard);
            UserStateStorage.AddMessage(callbackQuery.From.Id, currnetMessage.MessageId);
        }
        public async Task Send(Message message, string query=null, bool sendedQuery=false)
        {
            var officeResponse = new GetOfficeListQueryResponse();
            if (query != null)
            {
                officeResponse = await _mediator.Send(new GetOfficeListQueryRequest()
                {
                    SearchBy = query,
                });
            }
            else
            {
                officeResponse = await _mediator.Send(new GetOfficeListQueryRequest()
                {
                    SearchBy = message.Text,
                });
            }
            
            var offices = officeResponse.Results;
            var inlineKeyboard = SendOfficeListKeyboard.BuildKeyboard(offices);
            var text = sendedQuery && string.IsNullOrEmpty(query) ? "Sorry, it seems I can not identify your location but you can filter offices there." : "Choose Office or type for filtering";
            var currnetMessage = await _bot.SendTextMessageAsync(message.Chat.Id, text, replyMarkup: inlineKeyboard);
            UserStateStorage.AddMessage(message.From.Id, currnetMessage.MessageId);
        }
    }
}
