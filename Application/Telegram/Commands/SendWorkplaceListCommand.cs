using Application.Features.CountryCQ;
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

namespace Application.Telegram
{
    public class SendWorkplaceListCommand
    {
        public TelegramBotClient _bot;
        public readonly IMediator _mediator;
        public SendWorkplaceListCommand(IMediator mediator,TelegramBotClient bot)
        {
            _bot = bot;
            _mediator = mediator;

        }
        public async Task Send(Message message)
        {
            var workplaceResponse = await _mediator.Send(new GetWorkplaceListQueryRequest());
            var workplaces = workplaceResponse.Results;
            var inlineKeyboard = SendWorkplaceListKeyboard.BuildKeyboard(workplaces);
            await _bot.SendTextMessageAsync(message.Chat.Id,"Workplace List",replyMarkup: inlineKeyboard);
        }

        public async Task SendListByMapId(CallbackQuery callbackQuery)
        {
            var workplaceResponse = await _mediator.Send(new GetWorkplaceListQueryRequest() { MapId = callbackQuery.Data.Split("|")[0]});
            var workplaces = workplaceResponse.Results;
            var inlineKeyboard = SendWorkplaceListKeyboard.BuildKeyboard(workplaces);
            await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Workplace List", replyMarkup: inlineKeyboard);
        }

    }
}






