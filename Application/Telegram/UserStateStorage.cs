using Application.Telegram.Models;
using Domain.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Telegram
{
    public static class UserStateStorage
    {
        public static ConcurrentDictionary<long, UserInfo> userInfo = new ConcurrentDictionary<long, UserInfo>() 
        {
            
        };
        public static UserState GetUserCurrentState(long telegramId)
        {
            UserInfo currnetUserInfo;
            bool exist = userInfo.TryGetValue(telegramId, out currnetUserInfo);
            if (exist)
            {
                return currnetUserInfo.CurrentState;
            }
            return UserState.StartingProcess;    
        }

        public static UserRole GetUserRole(long telegramId)
        {
            UserInfo currnetUserInfo;
            bool exist = userInfo.TryGetValue(telegramId, out currnetUserInfo);
            if (exist)
            {
                return currnetUserInfo.Role;
            }
            return UserRole.User;
        }
        public static void UserStateUpdate(long telegramId, UserState newState)
        {
            UserRole currnetUserRole = GetUserRole(telegramId);
            userInfo[telegramId] = new UserInfo() { CurrentState = newState, Role = currnetUserRole };
        }

        public static void AddUser(long telegramId, UserState state, UserRole role)
        {

            userInfo.TryAdd(telegramId, new UserInfo() { CurrentState = state, Role = role });
        }

    }
}
