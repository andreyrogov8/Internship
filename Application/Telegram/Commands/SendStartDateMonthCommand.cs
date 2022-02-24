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
    public class SendStartDateMonthCommand
    {
        public TelegramBotClient _bot;
        public readonly IMediator _mediator;
        public SendStartDateMonthCommand(IMediator mediator, TelegramBotClient bot)
        {
            _bot = bot;
            _mediator = mediator;

        }
        
        public async Task Send(CallbackQuery callbackQuery)
        {
            var inlineKeyboard = SendDateMonthKeyboard.BuildKeyboard();
            var currentMessage = await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, 
                "Please select start date month", 
                replyMarkup: inlineKeyboard);
            UserStateStorage.userInfo[callbackQuery.From.Id].Booking.WorkplaceId = Int32.Parse(callbackQuery.Data);
            UserStateStorage.AddMessage(callbackQuery.From.Id, currentMessage.MessageId);
        }
    }
}
