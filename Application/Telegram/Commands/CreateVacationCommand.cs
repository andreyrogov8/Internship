using Application.Features.UserFeature.Queries;
using Application.Features.VacationFeature.Commands;
using Application.Telegram.Keyboards;
using Domain.Enums;
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
        public async Task Send(Message message=null, CallbackQuery callbackQuery=null, bool enteredDate = false)
        {
            if (!enteredDate && callbackQuery is not null)
            {
                var currentMessage = await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Enter your starting date and Ending date as the following format: <code>year/month/day, year/month/day</code>\n first one will be starting and second one will be your ending date of vacation.", ParseMode.Html);
                UserStateStorage.AddMessage(callbackQuery.From.Id, currentMessage.MessageId);

                return;
            }
            else if (enteredDate && message is not null)
            {
                var messageText = message.Text;
                var DateTexts = messageText.Split(',');
                var startAndEndDates = new List<DateTimeOffset>();
                for (var i=0; i< 2;i ++)
                {
                    var year = int.Parse(DateTexts[i].Split('/')[0]);
                    var month = int.Parse(DateTexts[i].Split('/')[1]);
                    var date = int.Parse(DateTexts[i].Split('/')[2]);

                    var Date = new DateTimeOffset(new DateTime(year, month, date));
                    startAndEndDates.Add(Date);
                }
                var user = await _mediator.Send(new GetUserByIdQueryRequest{ TelegramId = message.From.Id});
                var vacation = await _mediator.Send(new CreateVacationCommandRequest { UserId=user.Id, VacationStart= startAndEndDates[0], VacationEnd = startAndEndDates[1]});
                var commandNames = new List<string> { "New Booking", "New Vacation", "My Bookings", "BACKProcessNotStarted" };
                var inlineKeyboard = CommandsListKeyboard.BuildKeyboard(commandNames, 2);
                var currentMessage = await _bot.SendTextMessageAsync(message.Chat.Id, "Your vacation has been created.", replyMarkup:inlineKeyboard);
                UserStateStorage.UserStateUpdate(message.From.Id, UserState.SelectingAction);
                UserStateStorage.AddMessage(message.From.Id, currentMessage.MessageId);
                return;
            }


            
        }
    }
}
