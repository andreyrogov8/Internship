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

namespace Application.Telegram.MainActions
{
    public class MyBookingsCommand
    {
        public static async Task HandleAsync(Update update, TelegramBotClient telegramBotClient, IMediator mediator)
        {
            var user = UserStateStorage.userInfo[update.CallbackQuery.From.Id];

            switch (user.CurrentState)
            {
                case UserState.CheckingBookings:
                    await new SendSelectedBookingCommand(mediator, telegramBotClient).SendSelectedBookingAsync(update.CallbackQuery, UserState.StartingProcess.ToString());
                    UserStateStorage.UpdateUserState(update.CallbackQuery.From.Id, UserState.CheckingSelectedBooking);
                    return;
            }
        }
    }
}
