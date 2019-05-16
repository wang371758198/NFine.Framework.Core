using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;





namespace NFine.Scheduler
{
    class Program
    {
        private static IConfiguration configuration;
        private static NameValueCollection nameValueCollection;
        static void Main(string[] args)
        {
            #region 配置                               /
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true)//增加环境配置文件，新建项目默认有
                .AddEnvironmentVariables();
            #endregion
            configuration = builder.Build();

            #region 设置scheduler创建参数          /
            nameValueCollection = new NameValueCollection();
            foreach (var config in configuration.GetSection("QuartzNet").GetChildren())
                nameValueCollection.Add(config.Key, config.Value);
            #endregion

            var servicesProvider = ConfigureServices(configuration);

            RunProgram(servicesProvider.GetService<StdSchedulerFactory>()).GetAwaiter().GetResult();
        }


        private static async Task RunProgram(StdSchedulerFactory factory)
        {
            try
            {
                IScheduler scheduler = await factory.GetScheduler();

                await scheduler.Start();

                // and last shut down the scheduler when you are ready to close your program
                //await scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }
        }


        private static IServiceProvider ConfigureServices(IConfiguration config) => new ServiceCollection()
            .AddSingleton(typeof(StdSchedulerFactory), new StdSchedulerFactory(nameValueCollection))
               .AddLogging(loggingBuilder =>
               {
                   // configure Logging with NLog
                   loggingBuilder.ClearProviders();
                   loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                   loggingBuilder.AddNLog(config);
               })
               .BuildServiceProvider();
    }
}
