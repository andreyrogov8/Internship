using Application.Features.UserFeature;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.UserFeature.Queries;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram
{
    public class SendUserListCommand : CommandBase
    {
        public SendUserListCommand(IMediator mediator, TelegramBotClient bot, Message message) : base(mediator, bot, message)
        {

        }
        public async Task Send()
        {
            var usersResponse = await _mediator.Send(new GetUserListQueryRequest());
            var users = usersResponse.Users;
            var buttons = new List<KeyboardButton>();

            foreach (var user in users)
            {
                buttons.Add(new KeyboardButton($"Id: {user.Id}, Name: {user.FirstName} {user.LastName}"));
            }
            var replyKeyboard = BuildKeyboard(buttons, 1);

            await _bot.SendTextMessageAsync(_message.Chat.Id, "User List", replyMarkup: replyKeyboard);
        }

    }
}