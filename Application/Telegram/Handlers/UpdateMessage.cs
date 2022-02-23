using Application.Telegram.Commands;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Application.Telegram.Handlers
{
    public static class UpdateMessage
    {
        public static async Task Handle(Update update, TelegramBotClient telegraBotClient, IMediator mediator)
        {
            await TelegramMessages.Delete(telegraBotClient, update.Message.From.Id);
            switch (UserStateStorage.GetUserCurrentState(update.Message.From.Id))
            {
                case UserState.ProcessNotStarted:
                    await new ProvideButtons(telegraBotClient).Send(
                                    update.Message, new List<string>() { "Start" }, 1);
                    UserStateStorage.UserStateUpdate(update.Message.From.Id, UserState.StartingProcess);
                    return;
                case UserState.StartingBooking:
                    await new SendOfficeListCommand(mediator, telegraBotClient).Send(update.Message);
                    UserStateStorage.UserStateUpdate(update.Message.From.Id, UserState.StartingBooking);
                    return;
            }
        }
    }
}
