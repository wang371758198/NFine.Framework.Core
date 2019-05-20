using Microsoft.Extensions.Logging;
using NFine.Application.SystemManage;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFine.Scheduler.Jobs
{
    public class HelloJob: IJob
    {
        private ILogger<HelloJob> logger;
        //private DutyApp dutyApp;
        public HelloJob(ILogger<HelloJob> logger)
        {
            this.logger = logger;
        }


        public async Task Execute(IJobExecutionContext context)
        {
            logger.LogInformation("Async Hello ！！！" + DateTime.Now.ToString("HH:mm:ss.fff"));
            await Console.Out.WriteLineAsync("Async Hello ！！！" + DateTime.Now.ToString("HH:mm:ss.fff"));
        }
    }
}
