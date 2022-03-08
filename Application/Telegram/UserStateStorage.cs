using Application.Features.BookingFeature.Commands;
using Application.Telegram.Models;
using Domain.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Application.Telegram
{
    public static class UserStateStorage
    {
        public static ConcurrentDictionary<long, UserInfo> userInfo = new ConcurrentDictionary<long, UserInfo>() 
        {
            
        };
        public static ConcurrentDictionary<long, List<int>> userMessages = new ConcurrentDictionary<long, List<int>>()
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
            return UserState.ProcessNotStarted;    
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
        public static void UpdateUserState(long telegramId, UserState newState)
        {
            UserRole currnetUserRole = GetUserRole(telegramId);
            userInfo[telegramId] = new UserInfo() { 
                CurrentState = newState, 
                Role = currnetUserRole,
                MapId = userInfo[telegramId].MapId, 
                UserDates = userInfo[telegramId].UserDates,
                WorkplaceId = userInfo[telegramId].WorkplaceId,
                UserId = userInfo[telegramId].UserId,
                HasWindow = userInfo[telegramId].HasWindow, 
                HasPc = userInfo[telegramId].HasPc,
                HasMonitor = userInfo[telegramId].HasMonitor,
                HasKeyboard = userInfo[telegramId].HasKeyboard,
                HasMouse = userInfo[telegramId].HasMouse,
                HasHeadset = userInfo[telegramId].HasHeadset,
                SelectedBookingId = userInfo[telegramId].SelectedBookingId,
            };
        }

        public static void AddUser(long telegramId, int userId, UserState state, UserRole role )
        {

            userInfo.TryAdd(telegramId, new UserInfo()
            {
                CurrentState = state,
                Role = role,               
                UserId = userId,
            });
        }

        public static void AddRecordToUserMessages(long telegramId, List<int> messageList)
        {
            userMessages.TryAdd(telegramId, messageList);
        }
        public static void AddMessage(long telegramId,int messageId)
        {
            var userMessageList = GetUserMessages(telegramId);
            userMessageList.Add(messageId);
            userMessages[telegramId] = userMessageList;
        }
        public static void RemoveMessages(long telegramId)
        {
            var userMessageList = GetUserMessages(telegramId);
            userMessageList.Clear();
            userMessages[telegramId] = userMessageList;
        }        
        public static List<int> GetUserMessages(long telegramId)
        {
            List<int> userMessageList;
            bool exist = userMessages.TryGetValue(telegramId, out userMessageList);
            if (exist)
            {                
                return userMessageList;
            }
            return null;
        }

    }
}
