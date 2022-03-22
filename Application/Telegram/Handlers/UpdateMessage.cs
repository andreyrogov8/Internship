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
            switch (UserStateStorage.GetUserCurrentState(update.Message.From.Id))
            {
                case UserState.ProcessNotStarted:
                    await Helper.DeleteMessageAsync(telegraBotClient, update.Message.From.Id);
                    UserStateStorage.UpdateUserState(update.Message.From.Id, UserState.StartingProcess);
                    await new StartCommand(mediator, telegraBotClient).HandleAsync(update);
                    return;
                case UserState.NewBookingIsSelectedStartingBooking:
                    await Helper.DeleteMessageAsync(telegraBotClient, update.Message.From.Id);
                    await new SendOfficeListCommand(mediator, telegraBotClient).Send(update.Message);
                    UserStateStorage.UpdateUserState(update.Message.From.Id, UserState.NewBookingIsSelectedStartingBooking);
                    return;
                case UserState.EnteringLocation:
                    await Helper.DeleteMessageAsync(telegraBotClient, update.Message.From.Id);
                    await new ReceiveUserLocationCommand(mediator, telegraBotClient, clientFactory).ReceiveLocationAndSendOffices(update.Message);
                    return;
                default:
                    await telegraBotClient.DeleteMessageAsync(update.Message.From.Id, update.Message.MessageId);                    
                    return;
            }
        }
    }
}
