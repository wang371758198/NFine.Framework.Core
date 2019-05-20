using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace NFine.Scheduler
{
   
    // Quartz.Net启动后注册job和trigger
    public class QuartzStartup
    {
        private IScheduler _scheduler;
        public IScheduler Scheduler
        {
            get { return _scheduler; }
            set { _scheduler = value; }
        }

        private readonly ILogger _logger;
        private readonly IJobFactory iocJobfactory;
        public QuartzStartup(IServiceProvider IocContainer, ILoggerFactory loggerFactory, IConfiguration Configuration)
        {
            _logger = loggerFactory.CreateLogger<QuartzStartup>();
            iocJobfactory = new NFineJobFactory(IocContainer);

            #region 设置scheduler创建参数          /
            var nameValueCollection = new NameValueCollection();
            foreach (var config in Configuration.GetSection("QuartzNet").GetChildren())
                nameValueCollection.Add(config.Key, config.Value);
            #endregion
            
            var schedulerFactory = new StdSchedulerFactory(nameValueCollection);
            _scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();
            _scheduler.JobFactory = iocJobfactory;
        }

        public void Start()
        {
            _logger.LogInformation("Schedule job load as application start.");
            _scheduler.Start().GetAwaiter();

            //var UsageCounterSyncJob = JobBuilder.Create<Jobs.HelloJob>()
            //  .WithIdentity("UsageCounterSyncJob")
            //  .Build();

            //var UsageCounterSyncJobTrigger = TriggerBuilder.Create()
            //    .WithIdentity("UsageCounterSyncCron")
            //    .StartNow()
            //    // 每隔一小时同步一次
            //    .WithCronSchedule("0/2 * * * * ?")      // Seconds,Minutes,Hours，Day-of-Month，Month，Day-of-Week，Year（optional field）
            //    .Build();
            //_scheduler.ScheduleJob(UsageCounterSyncJob, UsageCounterSyncJobTrigger).Wait();

            //_scheduler.TriggerJob(new JobKey("UsageCounterSyncJob"));
        }

        public void Stop()
        {
            if (_scheduler == null)
            {
                return;
            }

            if (_scheduler.Shutdown(waitForJobsToComplete: true).Wait(30000))
                _scheduler = null;
            else
            {
            }
            _logger.LogCritical("Schedule job upload as application stopped");
        }
    }

    /// <summary>
    /// NFineJobFactory ：实现在Timer触发的时候注入生成对应的Job组件,使用ServiceProvider容器生成对象
    /// </summary>
    public class NFineJobFactory : IJobFactory
    {
        protected readonly IServiceProvider Container;

        public NFineJobFactory(IServiceProvider container)
        {
            Container = container;
        }

        //Called by the scheduler at the time of the trigger firing, in order to produce
        //     a Quartz.IJob instance on which to call Execute.
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return Container.GetService(bundle.JobDetail.JobType) as IJob;
        }

        // Allows the job factory to destroy/cleanup the job if needed.
        public void ReturnJob(IJob job)
        {
        }
    }
}
