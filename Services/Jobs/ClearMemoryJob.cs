﻿using Application.Telegram;
using Quartz;

namespace Services.Jobs
{
    public class ClearMemoryJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Job executed");
            var userInfoDict = UserStateStorage.userInfo;
            foreach(var user in userInfoDict)
            {
                if (CheckDatesGreaterThanFifteenMinutes(
                    user.Value.StartedActionDateTime, user.Value.CurrentDateTime))
                {
                    UserStateStorage.Remove(user.Key);
                }
            }

            foreach (var user in userInfoDict)
            {
                Console.WriteLine(user.Value.CurrentDateTime.ToString());
                Console.WriteLine(user.Value.StartedActionDateTime.ToString());

            }
        }


        public bool CheckDatesGreaterThanFifteenMinutes(DateTimeOffset startActionDatetime, DateTimeOffset currentDateTime)
        {
            // change to minutes
            return (currentDateTime - startActionDatetime).Seconds >= 15;
        }

        public bool CheckDatesGreaterThanFifteenMinutes(DateTimeOffset startActionDatetime)
        {
            var today = DateTimeOffset.Now;

            // change to minutes
            return (today - startActionDatetime).Seconds >= 15;
        }
    }
    

}
