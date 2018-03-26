using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace NFine.Code
{
    public static class Logger<T>
    {
        private static Logger _logger ;
        private static Logger _loggerError;
        static Logger()
        {
             _logger = NLog.LogManager.LoadConfiguration("NLog.config").GetLogger("Console."+typeof(T).ToString());
            _loggerError= NLog.LogManager.LoadConfiguration("NLog.config").GetLogger("Error." + typeof(T).ToString());
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

        public static void Error(string message, params object[] args) => _loggerError.Error(message, args);
        public static void Error(Exception e) => _loggerError.Error(e,e.Message);
        public static void Error(string message, Exception e) => _loggerError.Error(e, message);

    }
}
