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
        public static async Task Handle(Update update, TelegramBotClient telegraBotClient, IMediator mediator, IHttpClientFactory clientFactory)
        {
            await Helper.DeleteMessageAsync(telegraBotClient, update.Message.From.Id);
            switch (UserStateStorage.GetUserCurrentState(update.Message.From.Id))
            {
                case UserState.ProcessNotStarted:
                   UserStateStorage.UpdateUserState(update.Message.From.Id, UserState.StartingProcess);
                    await new StartCommand(mediator, telegraBotClient).HandleAsync(update);
                    return;
                case UserState.NewBookingIsSelectedStartingBooking:
                    await new SendOfficeListCommand(mediator, telegraBotClient).Send(update.Message);
                    UserStateStorage.UpdateUserState(update.Message.From.Id, UserState.NewBookingIsSelectedStartingBooking);
                    return;
                case UserState.EnteringLocation:
                    await new ReceiveUserLocationCommand(mediator, telegraBotClient, clientFactory).ReceiveLocationAndSendOffices(update.Message);
                    return;
            }
        }
    }
}
