using Application.Features.UserFeature.Queries;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Application.Telegram.Middleware
{
    public static class Authentication
    {
        public static async Task<bool> Authenticate(Update update, IMediator _mediator)
        {
            var isAuthorized = UserStateStorage.userInfo.ContainsKey(update.Message.From.Id);
            if (isAuthorized)
                return true;

            var user = await _mediator.Send(new GetUserByIdQueryRequest { TelegramId = update.Message.From.Id });           
            if (user == null)
                return false;

            UserRole role = (UserRole) Enum.Parse(typeof(UserRole), user.Roles.First());
            UserStateStorage.AddUser(update.Message.From.Id, UserState.ProcessNotStarted, role);
            return true;
        }
    }
}
