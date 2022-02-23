using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Application.Telegram.Commands
{
    public class CreateVacationCommand
    {
        private readonly IMediator _mediator;
        private readonly TelegramBotClient _bot;

        public CreateVacationCommand(IMediator mediator, TelegramBotClient bot)
        {
            _mediator = mediator;
            _bot = bot;

        }
        public async Task Send(CallbackQuery callbackQuery, bool enteredDate = false)
        {
            if (!enteredDate)
            {
                await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Enter your starting date and Ending date as the following format: <code>year/month/day, year/month/day</code>\n first one will be starting and second one will be your ending date of vacation.", ParseMode.Html);
                return;
            }

            
        }
    }
}
