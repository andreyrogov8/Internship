using Application.Telegram;
using Quartz;

namespace Services.Jobs
{
    public class ClearMemoryJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var userInfoDict = UserStateStorage.userInfo;
            foreach(var user in userInfoDict)
            {
                if (CheckDatesGreaterThanFifteenMinutes(
                    user.Value.CurrentDateTime))
                {
                    UserStateStorage.Remove(user.Key);
                }
            }            
        }


        

        public bool CheckDatesGreaterThanFifteenMinutes(DateTimeOffset startActionDatetime)
        {
            var today = DateTimeOffset.UtcNow;

            return (today - startActionDatetime).Minutes > 15;
        }
    }
    

}
