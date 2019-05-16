using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Scheduler.Jobs
{
    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Async Hello ！！！"+DateTime.Now.ToString("HH:mm:ss.fff"));
        }
    }
}
