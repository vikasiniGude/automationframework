using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Config;
using log4net;
using log4net.Repository.Hierarchy;

namespace APIRestSharpCoreLayer.Utils
{
    public class Log4NetLogger
    {
        public static ILog log = LogManager.GetLogger(typeof(Log4NetLogger));
        static Log4NetLogger()
        {
            var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            log = LogManager.GetLogger(typeof(Logger));
        }
        public static void Info(string message) => log.Info(message);
        public static void Debug(string message) => log.Debug(message);
        public static void Error(string message) => log.Error(message);
        public static void Warn(string message) => log.Warn(message);

        
    }
}
