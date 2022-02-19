using Application.Features.BookingFeature.Queries;
using Application.Features.CountryCQ;
using Application.Interfaces;
using Application.Telegram;
using Application.Telegram.Commands;
using Application.Telegram.Models;
using Domain.Enums;
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
            //for testing
            if (UserStateStorage.GetUserCurrentState(update.Message.From.Id) == UserState.StartingProcess)
            {
                UserStateStorage.AddUser(5213829376, UserState.StartingProcess,UserRole.User );
            }
            
            await _telegraBotClient.SendChatActionAsync(update.Message.Chat.Id, ChatAction.Typing);
            if (update.Message != null)
            {
                switch (UserStateStorage.GetUserCurrentState(update.Message.From.Id))
                {
                    case UserState.StartingProcess:
                        UserStateStorage.UserStateUpdate(update.Message.From.Id, UserState.SelectingAction);
                        await new ProvideButtons(_telegraBotClient, update.Message).Send();
                        return;
                    case UserState.SelectingAction:
                        UserStateStorage.UserStateUpdate(update.Message.From.Id, UserState.ActionIsSelected);
                        List<string> buttons = new List<string>(){"New Booking","My Bookings"};
                        await new ProvideButtons(_telegraBotClient, update.Message).Send(buttons);
                        return;
                    case UserState.ActionIsSelected:
                        {
                            switch (update.Message.Text)
                            {
                                case "New Booking":
                                    UserStateStorage.UserStateUpdate(update.Message.From.Id, UserState.StartingBooking);
                                    await new SendOfficeListCommand(_mediator, _telegraBotClient, update.Message).Send();
                                    return;
                                case "My Bookings":
                                    UserStateStorage.UserStateUpdate(update.Message.From.Id, UserState.CheckingBookings);
                                    await new SendOfficeListCommand(_mediator, _telegraBotClient, update.Message).Send();
                                    return;
                            }
                            return;
                        }
                    case UserState.StartingBooking:
                        if (update.Message.Text.Contains(":"))
                        {
                            //provide workplaces for this office
                            UserStateStorage.UserStateUpdate(update.Message.From.Id, UserState.SelectingWorkplace);
                            await new ProvideButtons(_telegraBotClient, update.Message).Send();
                        }
                        else
                        {
                            //provide offices based on fiter condition
                            UserStateStorage.UserStateUpdate(update.Message.From.Id, UserState.StartingBooking);
                            await new SendOfficeListCommand(_mediator, _telegraBotClient, update.Message, update.Message.Text).Send();
                            return;
                        }
                        
                        return;
                }
            }            
        }




    }
}
