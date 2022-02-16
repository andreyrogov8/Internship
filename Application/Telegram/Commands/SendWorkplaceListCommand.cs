﻿using Application.Features.CountryCQ;
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
    public class SendWorkplaceListCommand : CommandBase
    {
        public SendWorkplaceListCommand(IMediator mediator,TelegramBotClient bot, Message message) : base(mediator,bot, message)
        {

        }
        public async Task Send()
        {
            var workplaceResponse = await _mediator.Send(new GetWorkplaceListQueryRequest());
            var workplaces = workplaceResponse.Results;
            var buttons = new List<KeyboardButton>();

            foreach (var workplace in workplaces)
            {
                buttons.Add(new KeyboardButton($"Id: {workplace.Id}, WorkplaceNumber: {workplace.WorkplaceNumber}"));
            }
            var replyKeyboard = BuildKeyboard(buttons, 2);

            await _bot.SendTextMessageAsync(_message.Chat.Id,"Workplace List",replyMarkup: replyKeyboard);
        }

    }
}






