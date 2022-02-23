﻿using Application.Telegram.Commands;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Application.Telegram.Commands;
namespace Application.Telegram.Handlers
{
    public static class UpdateMessage
    {
        public static async Task Handle(Update update, TelegramBotClient telegraBotClient, IMediator mediator)
        {
            switch (UserStateStorage.GetUserCurrentState(update.Message.From.Id))
            {
                case UserState.ProcessNotStarted:
                    //await new ProvideButtons(telegraBotClient).Send(
                    //                update.Message, new List<string>() { "Start" }, 1);
                    UserStateStorage.UserStateUpdate(update.Message.From.Id, UserState.StartingProcess);
                    await new StartCommand(mediator, telegraBotClient).Handle(update);
                    return;
                case UserState.StartingBooking:
                    await new SendOfficeListCommand(mediator, telegraBotClient).Send(update.Message);
                    UserStateStorage.UserStateUpdate(update.Message.From.Id, UserState.StartingBooking);
                    return;
            }
        }
    }
}
