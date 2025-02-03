using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Config;
using log4net;
using log4net.Repository.Hierarchy;
using log4net.Appender;

namespace Automation_CoreLayer.Utils
{
    public static class Log4NetLogger
    {
        private static ILog log;
        private static FileAppender fileAppender;
        static Log4NetLogger()
        {
            log = LogManager.GetLogger(typeof(Log4NetLogger));
            var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());            
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            log = LogManager.GetLogger(typeof(Logger));
            fileAppender = logRepository.GetAppenders().OfType<log4net.Appender.FileAppender>().FirstOrDefault();
        }
        public static void SetFilePath(string filePath)
        {
            if (fileAppender != null)
            {
                fileAppender.File = @$"{filePath}";
                fileAppender.ActivateOptions();
            }
        }
        public static void Info(string message) => log.Info(message);
        public static void Debug(string message) => log.Debug(message);
        public static void Error(string message) => log.Error(message);
        public static void Warn(string message) => log.Warn(message);


    }
}
