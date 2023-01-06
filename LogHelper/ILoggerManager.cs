using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReaderCMS.LogHelper
{
   public interface ILoggerManager
    {
        void LogError(string ErrorMessage);
        void LogInfo(string Message);
        void LogTrace(string Message);
        void LogErrorCsv(string ErrorMessage, Exception ex);
        void LogErrorSqlServer(string ErrorMessage,Exception ex);
    }
}
