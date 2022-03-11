using Application.Features.CountryCQ;
using Application.Features.MapFeature.Queries;
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
        public async Task SendAsync(Message message)
        {
            var workplaceResponse = await _mediator.Send(new GetWorkplaceListQueryRequest());
            var workplaces = workplaceResponse.Results;
            var inlineKeyboard = SendWorkplaceListKeyboard.BuildKeyboard(workplaces);
            var currentMessage = await _bot.SendTextMessageAsync(message.Chat.Id,"Workplace List",replyMarkup: inlineKeyboard);
            UserStateStorage.AddMessage(message.From.Id, currentMessage.MessageId);
        }

        public async Task SendListByMapIdWithAttributesAsync(CallbackQuery callbackQuery)
        {
            var workplaceResponse = await _mediator.Send(
                new GetWorkplaceListQueryRequest() 
                {
                    MapId = UserStateStorage.userInfo[callbackQuery.From.Id].MapId.ToString(),
                    StartDate = Helper.GetStartDate(callbackQuery),
                    EndDate = Helper.GetEndDate(callbackQuery),
                    HasWindow = UserStateStorage.userInfo[callbackQuery.From.Id].HasWindow,
                    HasPc = UserStateStorage.userInfo[callbackQuery.From.Id].HasPc,
                    HasMonitor = UserStateStorage.userInfo[callbackQuery.From.Id].HasMonitor,
                    HasKeyboard = UserStateStorage.userInfo[callbackQuery.From.Id].HasKeyboard,
                    HasMouse = UserStateStorage.userInfo[callbackQuery.From.Id].HasMouse,
                    HasHeadset = UserStateStorage.userInfo[callbackQuery.From.Id].HasHeadset,
                    RecurringDay = UserStateStorage.userInfo[callbackQuery.From.Id].RecurringDay
                });
            
            var workplaces = workplaceResponse.Results;
            var inlineKeyboard = SendWorkplaceListKeyboard.BuildKeyboard(workplaces);
            var map = await _mediator.Send(new GetMapByIdQueryRequest() { Id = UserStateStorage.userInfo[callbackQuery.From.Id].MapId });
            var currentMessage = await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id
                , callbackQuery.Data.Contains("BACK") ? "Please choose workplace" : "Please choose workplace"
                , replyMarkup: inlineKeyboard);
            UserStateStorage.AddMessage(callbackQuery.From.Id, currentMessage.MessageId);
        }

        public async Task SendListByMapIdAsync(CallbackQuery callbackQuery)
        {
            var workplaceResponse = await _mediator.Send(
                new GetWorkplaceListQueryRequest()
                {
                    MapId = UserStateStorage.userInfo[callbackQuery.From.Id].MapId.ToString(),
                    StartDate = Helper.GetStartDate(callbackQuery),
                    EndDate = Helper.GetEndDate(callbackQuery),
                    RecurringDay = UserStateStorage.userInfo[callbackQuery.From.Id].RecurringDay
                });

            var workplaces = workplaceResponse.Results;
            var inlineKeyboard = SendWorkplaceListKeyboard.BuildKeyboard(workplaces);
            var map = await _mediator.Send(new GetMapByIdQueryRequest() { Id = UserStateStorage.userInfo[callbackQuery.From.Id].MapId });
            var currentMessage = await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id
                , callbackQuery.Data.Contains("BACK") ? "Please choose workplace" : "Please choose workplace"
                , replyMarkup: inlineKeyboard);
            UserStateStorage.AddMessage(callbackQuery.From.Id, currentMessage.MessageId);
        }
    }
}






