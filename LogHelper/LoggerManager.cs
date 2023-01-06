using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.LogHelper
{
    public class LoggerManager : ILoggerManager
    {
        Logger log = NLog.LogManager.GetLogger("LogFile");
        Logger logCsv = NLog.LogManager.GetLogger("CsvLog");
        Logger logErrorWithSqlServer = NLog.LogManager.GetLogger("logErrorWithSqlServer");

        public void LogError(string ErrorMessage)
        {
            log.Error(ErrorMessage);
        }

        public void LogErrorCsv(string ErrorMessage, Exception ex)
        {
            logCsv.SetProperty("CustomProperty", "this custom message");
            logCsv.Error(ex, ErrorMessage);
        }

        public void LogErrorSqlServer(string ErrorMessage, Exception ex)
        {
            logErrorWithSqlServer.SetProperty("CustomProperty", "this is message");
            logErrorWithSqlServer.Error(ex, ErrorMessage);
        }

        public void LogInfo(string Message)
        {
            log.Info(Message);
        }

        public void LogTrace(string Message)
        {
            log.Trace(Message);
        }
    }
}