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
        public SendOfficeListCommand(IMediator mediator, TelegramBotClient bot)
        {
            _bot = bot;
            _mediator = mediator;

        }
        public async Task Send(CallbackQuery callbackQuery)
        {
            var officeResponse = await _mediator.Send(new GetOfficeListQueryRequest());
            var offices = officeResponse.Results;
            var buttons = new List<InlineKeyboardButton>();

            foreach (var office in offices)
            {
                buttons.Add(new InlineKeyboardButton($"Name: {office.Name}") { CallbackData =  office.Id.ToString() });
            }
            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, 2);

            await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Choose Office or type for filtering", replyMarkup: inlineKeyboard);
        }
        public async Task Send(Message message)
        {
            var officeResponse = await _mediator.Send(new GetOfficeListQueryRequest()
            {
                SearchBy = message.Text,
            });
            var offices = officeResponse.Results;
            var buttons = new List<InlineKeyboardButton>();

            foreach (var office in offices)
            {
                buttons.Add(new InlineKeyboardButton($"Name: {office.Name}") { CallbackData = office.Id.ToString() });
            }
            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, 2);

            await _bot.SendTextMessageAsync(message.Chat.Id, "Choose Office or type for filtering", replyMarkup: inlineKeyboard);
        }
    }
}
