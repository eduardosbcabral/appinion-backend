using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appinion.Api.Helper
{
    public class Quartz
    {
        private IScheduler _scheduler;

        public static IScheduler Scheduler { get { return Instance._scheduler; } }

        private static Quartz _instance = null;

        public static Quartz Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Quartz();
                }
                return _instance;
            }
        }

        private Quartz()
        {
            Init();
        }

        private async void Init()
        {
            _scheduler = await new StdSchedulerFactory().GetScheduler();
        }

        public IScheduler UseJobFactory(IJobFactory jobFactory)
        {
            Scheduler.JobFactory = jobFactory;
            return Scheduler;
        }

        public async void AddJob<T>(string name, string group, int interval)
            where T : IJob
        {
            IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity(name, group)
                .Build();

            ITrigger jobTrigger = TriggerBuilder.Create()
                .WithIdentity(name + "Trigger", group)
                .StartNow() // Jetzt starten
                .WithSimpleSchedule(t => t.WithIntervalInSeconds(interval).RepeatForever()) // Mit wiederholung alle interval sekunden
                .Build();

            await Scheduler.ScheduleJob(job, jobTrigger);
        }

        public async void AddWeeklyJob<T>(string name, string group)
            where T : IJob
        {
            IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity(name, group)
                .Build();

            IScheduleBuilder scheduleBuilder = CronScheduleBuilder
                .CronSchedule("	0 0 3 ? * SUN *") // EVERY SUNDAY AT 03:00 AM
                .InTimeZone(TimeZoneInfo.Utc);

            ITrigger jobTrigger = TriggerBuilder.Create()
                .WithIdentity(name + "Trigger", group)
                .WithSchedule(scheduleBuilder)
                .StartNow()
                .Build();

            await Scheduler.ScheduleJob(job, jobTrigger);
        }



        public static async void Start()
        {
            await Scheduler.Start();
        }
    }
}
