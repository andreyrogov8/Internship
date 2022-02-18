using Application.Features.OfficeFeature.Queries;
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
        public string _searchBy;
        public Message _message;
        public SendOfficeListCommand(IMediator mediator, TelegramBotClient bot, Message message, string searchBy = null)
        {
            _bot = bot;
            _mediator = mediator;
            _message = message;
            _searchBy = searchBy;

        }
        public async Task Send()
        {
            var officeResponse = await _mediator.Send(new GetOfficeListQueryRequest() {
                SearchBy = _searchBy,
            });
            var offices = officeResponse.Results;
            var buttons = new List<KeyboardButton>();

            foreach (var office in offices)
            {
                buttons.Add(new KeyboardButton($"Name: {office.Name}"));
            }
            var replyKeyboard = KeyboardHelper.BuildKeyboard(buttons, 2);

            await _bot.SendTextMessageAsync(_message.Chat.Id, "Choose Office or type for filtering", replyMarkup: replyKeyboard);
        }
    }
}
