using Application.Features.CountriesFeature.Queries;
using Application.Telegram.Keyboards;
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
    public class SendMonthCommand
    {
        public TelegramBotClient _bot;
        public readonly IMediator _mediator;
        public SendMonthCommand(IMediator mediator, TelegramBotClient bot)
        {
            _bot = bot;
            _mediator = mediator;

        }
        
        public async Task SendAsync(CallbackQuery callbackQuery, string outputText, string backButtonCallBackData, string typeOfProcess)
        {
            var inlineKeyboard = SendDateMonthKeyboard.BuildKeyboard(callbackQuery,backButtonCallBackData, typeOfProcess);
            var currentMessage = await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id,
                outputText,
                replyMarkup: inlineKeyboard);
            UserStateStorage.AddMessage(callbackQuery.From.Id, currentMessage.MessageId);
        }
    }
}
