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
        public readonly IMediator _mediator;
        public SendMapListCommand(IMediator mediator, TelegramBotClient bot)
        {
            _bot = bot;
            _mediator = mediator;

        }
        public async Task Send(CallbackQuery callbackQuery)
        {
            var mapResponse = await _mediator.Send(new GetMapListQueryRequest() { OfficeId = callbackQuery.Data});
            var maps = mapResponse.Results;
            var buttons = new List<InlineKeyboardButton>();

            foreach (var map in maps)
            {
                buttons.Add(new InlineKeyboardButton($"Floor: {map.FloorNumber}") { CallbackData = map.OfficeId.ToString()});
            }
            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, 2);

            await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Choose Floor in Office", replyMarkup: inlineKeyboard);
        }

    }
}
