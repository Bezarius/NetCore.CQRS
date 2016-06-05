using System;
using NLog;
using ILogger = OnionCQRS.Core.Logging.ILogger;

namespace OnionCQRS.Infrastructure.Logging
{
    public class NLogLogger : ILogger
    {
        private static readonly Lazy<NLogLogger> LazyLogger = new Lazy<NLogLogger>(() => new NLogLogger());
        private static readonly Lazy<Logger> LazyNLogger = new Lazy<Logger>(LogManager.GetCurrentClassLogger);

        public static ILogger Instance => LazyLogger.Value;

        private NLogLogger()
        {
        }

        public void Log(string message)
        {
            LazyNLogger.Value.Info(message);
        }

        public void Log(Exception ex)
        {
            LazyNLogger.Value.Error(ex);
        }
    }
}
