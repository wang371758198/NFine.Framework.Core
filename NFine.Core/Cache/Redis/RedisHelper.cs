using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;
using System.Net;

namespace NFine.Code
{
   public static class RedisHelper
    {

        private static ConfigurationOptions _configurationOptions;

        private static string _prefix = string.Empty;
        /// <summary>
        /// Redis配置
        /// </summary>
        public static ConfigurationOptions ConfigurationOptions
        {
            get { return _configurationOptions; }
        }


        static RedisHelper()
        {
            var _host = ConfigurationManager.Redis["host"];
            if (string.IsNullOrWhiteSpace(_host))
                throw new ArgumentNullException("appsettings.json中的redis对象的host字段不能为空");

            var _port = ConfigurationManager.Redis["port"];
            if (string.IsNullOrWhiteSpace(_port))
                throw new ArgumentNullException("appsettings.json中的redis对象的port字段不能为空");

             _prefix = ConfigurationManager.Redis["prefix"];

            _configurationOptions = new ConfigurationOptions()
            {
                 EndPoints = { { _host, Convert.ToInt32(_port) } },
                 ChannelPrefix=  new RedisChannel("NFine.Core.Web", RedisChannel.PatternMode.Pattern)
                // ConfigurationChannel="NFine.Core.Web.Config"


            };
        }

        public static RedisClient CreateRedisClient(string clientName)
        {
            _configurationOptions.ClientName = clientName;
            return new RedisClient( ConnectionMultiplexer.Connect(ConfigurationOptions),_prefix+clientName+".");
        }

        public static RedisCounter CreateRedisCounter(string clientName)
        {
            _configurationOptions.ClientName = clientName;
           return new RedisCounter(ConnectionMultiplexer.Connect(ConfigurationOptions),_prefix+clientName+".");
        }


    }
}
