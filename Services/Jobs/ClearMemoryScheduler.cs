using Application.Configurations;
using Quartz;
using Quartz.Impl;

namespace Services.Jobs
{
    public class ClearMemoryScheduler
    {
        public static async Task Start(IServiceProvider service, SchedulerConfigurations schedulerConfig)
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            scheduler.JobFactory = (JobFactory)service.GetService(typeof(JobFactory));
            await scheduler.Start();

            IJobDetail jobDetail = JobBuilder.Create<ClearMemoryJob>().Build();


            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("ClearMemory", "default")
                //.StartAt(
                    //DateBuilder.DateOf(
                    //schedulerConfig.startHour,
                    //schedulerConfig.startMinute,
                    //schedulerConfig.startSecond,
                    //schedulerConfig.startDay,
                    //schedulerConfig.startMonth))
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(24)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);

        }
    }
}
