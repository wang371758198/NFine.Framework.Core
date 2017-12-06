using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NFine.Code
{
   public class ConfigurationManager
    {
        private static IConfiguration config;
        static ConfigurationManager()
        {
             config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        }

        /// <summary>
        /// 获取appsettings.json 中appSettings配置
        /// </summary>
        public static IConfigurationSection AppSettings
        {
            get
            {
                return config.GetSection("appSettings");
            }
        }

        public static IConfigurationSection ConnectionStrings
        {
            get
            {
                return config.GetSection("connectionStrings");
            }
        }

    }
}
