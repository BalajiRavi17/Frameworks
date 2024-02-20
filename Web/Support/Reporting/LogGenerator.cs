using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;

namespace WebAutomation.Support.Reporting
{
    public class LogGenerator
    {

        private LogGenerator() { }

        private static LogGenerator instance;
        private static readonly object _lock = new object();
        public static LogGenerator getInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                { 
                    if (instance == null)
                    {
                        instance = new LogGenerator();
                    }
            }
        }
            return instance;
        }

        public ILog GenerateLogs(Type type)
        {
            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%date{Absolute} [%level] [%class] [%method] %message %newline";
            patternLayout.ActivateOptions();

            ConsoleAppender consoleAppender = new ConsoleAppender()
            {
                Name = "ConsoleAppender",
                Layout = patternLayout,
                Threshold = log4net.Core.Level.All
            };
            consoleAppender.ActivateOptions();
            FileAppender fileAppender = new FileAppender()
            {
                Name = "FileAppender",
                Layout = patternLayout,
                Threshold = log4net.Core.Level.All,
                AppendToFile = false,
                File = "FileAppender.log"
            };
            fileAppender.ActivateOptions();

            RollingFileAppender rollingFileAppender = new RollingFileAppender()
            {
                Name = "RollingFileAppender",
                Layout = patternLayout,
                Threshold = log4net.Core.Level.All,
                File = "RollingFileAppender.log",
                MaximumFileSize = "1MB",
                MaxSizeRollBackups = 10
            };
            rollingFileAppender.ActivateOptions();

            BasicConfigurator.Configure(consoleAppender, fileAppender, rollingFileAppender);

            ILog Logger = LogManager.GetLogger(type);

            return Logger;
        }
    }
}
