﻿using Application.Features.BookingFeature.Queries;
using Application.Features.CountryCQ;
using Application.Interfaces;
using Application.Telegram;
using Application.Telegram.Commands;
using Application.Telegram.Handlers;
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
                    UserStateStorage.AddUser(5213829376, UserState.ProcessNotStarted, UserRole.User );
                    UserStateStorage.AddRecordToUserMessages(5213829376, new List<int>());
                }
            }
                       
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    if (update.CallbackQuery.Data.Contains("BACK"))
                    {                        
                        UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id,
                            (UserState)Enum.Parse(typeof(UserState), update.CallbackQuery.Data.Substring(4)));
                    }
                    await UpdateCallbackQuery.Handle(update, _telegraBotClient, _mediator);
                    return;
                case UpdateType.Message:
                    await UpdateMessage.Handle(update, _telegraBotClient, _mediator);
                    return;
            }
        }
    }
}
