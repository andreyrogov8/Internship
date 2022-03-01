using Application.Exceptions;

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
        public TelegramCommunicationService(IConfiguration configuration, IMediator mediator)
        {
            _telegraBotClient = new TelegramBotClient(configuration["Token"]);
            _mediator   = mediator;
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
                //await _telegraBotClient.SendChatActionAsync(update.Message.Chat.Id, ChatAction.Typing);
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
                        await UpdateCallbackQuery.Handle(update, _telegraBotClient, _mediator);
                        return;
                    case UpdateType.Message:
                        await UpdateMessage.Handle(update, _telegraBotClient, _mediator);
                        return;
                }
            }
            catch (NotFoundException exeption)
            {
                switch (update.Type)
                {
                    case UpdateType.CallbackQuery:
                        await _telegraBotClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, exeption.Message);
                        return;
                    case UpdateType.Message:
                        await _telegraBotClient.SendTextMessageAsync(update.Message.Chat.Id, exeption.Message);
                        return;
                }
            }

            catch (ValidationException exeption)
            {
                switch (update.Type)
                {
                    case UpdateType.CallbackQuery:
                        await _telegraBotClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, exeption.Message);
                        return;
                    case UpdateType.Message:
                        await _telegraBotClient.SendTextMessageAsync(update.Message.Chat.Id, exeption.Message);
                        return;
                }
            }
        }
    }
}
