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

        public static bool Remove(long telegramId)
        {
            UserInfo currentUserInfo;
            return userInfo.TryRemove(telegramId,out currentUserInfo);
        }

        public static void Clear(long telegramId)
        {
            userInfo[telegramId].MapId = 0;
            userInfo[telegramId].WorkplaceId = 0;
            userInfo[telegramId].HasWindow = false;
            userInfo[telegramId].HasPc = false;
            userInfo[telegramId].HasMonitor = false;
            userInfo[telegramId].HasKeyboard = false;
            userInfo[telegramId].HasMouse = false;
            userInfo[telegramId].HasHeadset = false;
            userInfo[telegramId].RecurringDay = null;
            userInfo[telegramId].RecurringDayWasNotFound = false;
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

        public static void UpdateUsersStartTime(long telegramId)
        {
            userInfo[telegramId].StartedActionDateTime = DateTime.UtcNow;
        }

        public static void UpdateUsersCurrentTime(long telegramId)
        {
            userInfo[telegramId].CurrentDateTime = DateTime.UtcNow;
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
                RecurringDay = userInfo[telegramId].RecurringDay,
                RecurringDayWasNotFound = userInfo[telegramId].RecurringDayWasNotFound,
                StartedActionDateTime = userInfo[telegramId].StartedActionDateTime,
                CurrentDateTime = userInfo[telegramId].CurrentDateTime,
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
