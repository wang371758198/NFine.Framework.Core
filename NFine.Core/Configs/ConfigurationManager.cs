using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NFine.Code
{
   public static class ConfigurationManager
    {
        private static IConfiguration config;

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

        public static void Configure(IConfiguration configuration)
        {
            config = configuration;
        }

    }
}
