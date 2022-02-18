using Application.Features.BookingFeature.Queries;
using Application.Features.CountryCQ;
using Application.Interfaces;
using Application.Telegram;
using Application.Telegram.Commands;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.TelegramBot
{
    public class TelegramCommunicationService : ITelegramCommunicationService
    {
        private readonly TelegramBotClient _telegraBotClient;
        private readonly IMediator _mediator;
        public TelegramCommunicationService(IConfiguration configuration, IMediator mediator)
        {
            _telegraBotClient = new TelegramBotClient(configuration["Token"]);
            _mediator   = mediator;
        }

        public async Task Execute(Update update)
        {
            await _telegraBotClient.SendChatActionAsync(update.Message.Chat.Id, ChatAction.Typing);
            if (update.Message != null)
            {
                switch (update.Message.Text)
                {
                    case "Start":
                        await new StartCommand(_telegraBotClient, update.Message).Send();
                        break;
                    case "NewBooking":                       
                        await new SendOfficeListCommand(_mediator, _telegraBotClient, update.Message,null,5).Send();
                        break;
                    case "SearchOfficeBy":

                        //await new SendOfficeListCommand(_mediator, _telegraBotClient, update.Message, null, 5).Send();
                        break;                        
                    case "getworkplaces":
                        await new SendWorkplaceListCommand(_mediator, _telegraBotClient, update.Message).Send();
                        break;
                    case "getbookings":
                        await new SendBookingListCommand(_mediator, _telegraBotClient, update.Message).Send();
                        break;
                    case "users":
                        await new SendUserListCommand(_mediator, _telegraBotClient, update.Message).Send();
                        break;
                    default:
                        await new DefaultHandler(_telegraBotClient, update.Message).Send();
                        break;
                }
                return;
            }            
        }




    }
}
