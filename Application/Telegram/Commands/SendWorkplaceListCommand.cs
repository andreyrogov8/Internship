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
        public Message _message;
        public readonly IMediator _mediator;
        public SendWorkplaceListCommand(IMediator mediator,TelegramBotClient bot, Message message)
        {
            _bot = bot;
            _message = message;
            _mediator = mediator;   
        }
        public async Task Send()
        {
            var workplaceResponse = await _mediator.Send(new GetWorkplaceListQueryRequest());
            var workplaces = workplaceResponse.Results;
            var replyKeyboard = SendWorkplaceListKeyboard.BuildKeyboard(workplaces);

            await _bot.SendTextMessageAsync(_message.Chat.Id,"Workplace List",replyMarkup: replyKeyboard);
        }

    }
}






