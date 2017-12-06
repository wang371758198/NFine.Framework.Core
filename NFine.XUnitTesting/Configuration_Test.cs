using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace NFine.XUnitTesting
{

    /// <summary>
    /// 配置文件的单元测试
    /// </summary>
   public class Configuration_Test
    {
        [Fact]
        public void Read()
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
    }
}
