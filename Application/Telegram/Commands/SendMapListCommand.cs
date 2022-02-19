using Application.Features.MapFeature.Queries;
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
        public Message _message;
        public readonly IMediator _mediator;
        public SendMapListCommand(IMediator mediator, TelegramBotClient bot, Message message)
        {
            _bot = bot;
            _message = message;
            _mediator = mediator;

        }
        public async Task Send(int? officeId)
        {
            var mapResponse = await _mediator.Send(new GetMapListQueryRequest() { OfficeId = officeId});
            var maps = mapResponse.Results;
            var buttons = new List<KeyboardButton>();

            foreach (var map in maps)
            {
                buttons.Add(new KeyboardButton($"Floor: {map.FloorNumber}"));
            }
            var replyKeyboard = KeyboardHelper.BuildKeyboard(buttons, 2);

            await _bot.SendTextMessageAsync(_message.Chat.Id, "Choose Floor in Office", replyMarkup: replyKeyboard);
        }

    }
}
