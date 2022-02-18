using Application.Telegram.Models;
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
        public static ConcurrentDictionary<int, UserInfo> userState = new ConcurrentDictionary<int, UserInfo>();

    }
}
