using Application.Features.UserFeature.Queries;
using Application.Interfaces;
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
        public static async Task<bool> AuthenticateAsync(Update update, IMediator _mediator)
        {
            var isAuthorized = UserStateStorage.userInfo.ContainsKey(update.Message.From.Id);
            if (isAuthorized)
                return true;

            GetUserByIdQueryResponse user = null;
            try
            {
                user = await _mediator.Send(new GetUserByIdQueryRequest { TelegramId = update.Message.From.Id });
                UserRole role = (UserRole)Enum.Parse(typeof(UserRole), user.Roles.First());
                UserStateStorage.AddUser(update.Message.From.Id, user.Id, UserState.ProcessNotStarted, role);
                UserStateStorage.AddRecordToUserMessages(update.Message.From.Id, new List<int>());
                return true;
            } 
            catch (Exception)
            {
                return false;
            }
        }
    }
}
