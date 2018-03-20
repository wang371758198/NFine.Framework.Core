using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using NFine.Code;
using Xunit;

namespace NFine.XUnitTesting.Redis
{
   public class Redis_Test
    {

        private void InitRedisConfig()
        {
            //AppDomain.CurrentDomain.BaseDirectory 运行的根目录
            //
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            basePath += @"appsettings.json";
            if (File.Exists(basePath))
            {
                System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

                string china = config.GetSection("appSettings")["China"];
                string aaaa = config.GetSection("appSettings")["aaaa"];
            }
            else
            {
                Console.WriteLine("");
            }
        }

        [Fact]
        public void Get()
        {
            InitRedisConfig();
            using (var client = RedisHelper.CreateRedisClient("Web"))
            {
               
              //  var result = client.Set("Demo","HelloWord!!!123111111111111"+Guid.NewGuid().ToString());
              
            }
        }

    }
}
