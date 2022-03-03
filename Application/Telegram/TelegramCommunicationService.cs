﻿using Application.Exceptions;

using Application.Interfaces;
using Application.Telegram;

using Application.Telegram.Handlers;
using Application.Telegram.Middleware;

using Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;

using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


namespace Application.TelegramBot
{
    public class TelegramCommunicationService : ITelegramCommunicationService
    {
        private readonly TelegramBotClient _telegraBotClient;
        private readonly IMediator _mediator;
        private readonly IHttpClientFactory _clientFactory;

        public TelegramCommunicationService(IConfiguration configuration, IMediator mediator, IHttpClientFactory clientFactory)
        {
            _telegraBotClient = new TelegramBotClient(configuration["Token"]);
            _mediator   = mediator;
            _clientFactory = clientFactory;
        }

        public async Task ExecuteAsync(Update update)
        {
            if (update.Message != null && !await Authentication.AuthenticateAsync(update, _mediator))
            {

                await _telegraBotClient.SendTextMessageAsync(update.Message.Chat.Id, "You are not authorized to use this bot");
                return;
            }

            try
            {
                switch (update.Type)
                {
                    case UpdateType.CallbackQuery:
                        if (update.CallbackQuery.Data.Contains("BACK"))
                        {
                            var state = update.CallbackQuery.Data.Substring(4);
                            UserState testState = (UserState)Enum.Parse(typeof(UserState), state);
                            UserStateStorage.UserStateUpdate(update.CallbackQuery.From.Id,
                                testState);
                        }
                        await UpdateCallbackQuery.Handle(update, _telegraBotClient, _mediator, _clientFactory);
                        return;
                    case UpdateType.Message:
                        await UpdateMessage.Handle(update, _telegraBotClient, _mediator, _clientFactory);
                        return;
                }
            }
            catch (NotFoundException exception)
            {
                await ShowExceptionAsync(update, exception.Message);
            }

            catch (ValidationException exception)
            {
                await ShowExceptionAsync(update, exception.Message);
            }

            
        }
        private async Task ShowExceptionAsync(Update update, string message)
        {
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    await _telegraBotClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, message);
                    return;
                case UpdateType.Message:
                    await _telegraBotClient.SendTextMessageAsync(update.Message.Chat.Id, message);
                    return;
            }
        }
    }
}
