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
            if (update.Message !=null)
            {
                if (UserStateStorage.GetUserCurrentState(update.Message.From.Id) == UserState.ProcessNotStarted)
                {
                    UserStateStorage.AddUser(5213829376, UserState.ProcessNotStarted, UserRole.User);
                }
            }

            
            //await _telegraBotClient.SendChatActionAsync(update.Message.Chat.Id, ChatAction.Typing);
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    await _telegraBotClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id,
                                                $"Button pressed at {DateTime.Now.ToString("h: mm:ss tt")} \n");
                    switch (UserStateStorage.GetUserCurrentState(update.CallbackQuery.From.Id))
                    {
                        case UserState.StartingProcess:
                            await new ProvideButtons(_telegraBotClient).Send(
                                update.CallbackQuery,new List<string>() { "New Booking", "My Bookings" }, 2);
                            UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.SelectingAction);
                            return;
                        case UserState.SelectingAction:
                            {
                                switch (update.CallbackQuery.Data)
                                {
                                    case "New Booking":
                                        await new SendOfficeListCommand(_mediator, _telegraBotClient).Send(update.CallbackQuery);
                                        UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.StartingBooking);
                                        return;
                                    //case "My Bookings":
                                    //    await new SendOfficeListCommand(_mediator, _telegraBotClient, update.Message).Send();
                                    //    UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.CheckingBookings);
                                    //    return;
                                }
                                return;
                            }
                        case UserState.StartingBooking:
                            await new SendMapListCommand(_mediator, _telegraBotClient).Send(update.CallbackQuery);
                            UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.SelectingFloor);
                            return;
                        case UserState.SelectingFloor:
                            await new SendWorkplaceListCommand(_mediator, _telegraBotClient).SendListByMapId(update.CallbackQuery);
                            UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id, UserState.SelectingWorkplace);
                            return;

                    }
                    return;
                case UpdateType.Message:
                    switch (UserStateStorage.GetUserCurrentState(update.Message.From.Id))
                    {
                        case UserState.ProcessNotStarted:
                            await new ProvideButtons(_telegraBotClient).Send(
                                            update.Message, new List<string>() { "Start" }, 1);
                            UserStateStorage.UserStateUpdate(update.Message.From.Id, UserState.StartingProcess);
                            return;
                        case UserState.StartingBooking:
                            await new SendOfficeListCommand(_mediator, _telegraBotClient).Send(update.Message);
                            UserStateStorage.UserStateUpdate(update.Message.From.Id, UserState.StartingBooking);
                            return;
                    }
                    return;
            }
        }
    }
}
