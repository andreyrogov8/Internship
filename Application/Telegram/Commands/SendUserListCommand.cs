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
using Application.Telegram;
using Application.Telegram.Keyboards;

namespace Application.Telegram
{
    public class SendUserListCommand
    {
        public TelegramBotClient _bot;
        public Message _message;
        public readonly IMediator _mediator;
        public SendUserListCommand(IMediator mediator, TelegramBotClient bot, Message message)
        {
            _bot = bot;
            _message = message;
            _mediator = mediator;
        }
        public async Task SendAsync()
        {
            var usersResponse = await _mediator.Send(new GetUserListQueryRequest());
            var users = usersResponse.Users;
            var inlineKeyboard = SendUserListKeyboard.BuildKeyboard(users);
            await _bot.SendTextMessageAsync(_message.Chat.Id, "User List", replyMarkup: inlineKeyboard);
        }

    }
}