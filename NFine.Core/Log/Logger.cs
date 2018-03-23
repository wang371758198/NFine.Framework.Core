using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace NFine.Code
{
    public static class Logger<T>
    {
        private static Logger _logger ;
        static Logger()
        {
             _logger = NLog.LogManager.LoadConfiguration("NLog.config").GetLogger(typeof(T).ToString());
        }

        public static void Trace(string message)
        {
            _logger.Trace(message);
        }

        public static void Info(string message)
        {
            _logger.Info(message);
        }

        public static void Info(string message, params object[] args)
        {
            _logger.Info(message, args);
        }


        public static void Error(string message)
        {
            _logger.Error(message);
        }

        public static void Error(string message, params object[] args) => _logger.Error(message, args);
        public static void Error(Exception e) => _logger.Error(e,e.Message);
        public static void Error(string message, Exception e) => _logger.Error(e, message);

    }
}
