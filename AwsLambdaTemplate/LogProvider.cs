using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwsLambdaTemplate
{
    public class LogProvider : ILoggerProvider
    {
        private readonly JsonLogger logger;

        public LogProvider(JsonLogger logger)
        {
            this.logger = logger;
        }

        public static bool Filter(LogLevel logLevel)
        {
            return logLevel > LogLevel.Warning;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new Logger(logger, categoryName);
        }

        public void Dispose()
        {
        }

        class Logger : ILogger
        {
            private readonly JsonLogger logger;
            private readonly string categoryName;

            public Logger(JsonLogger logger, string categoryName)
            {
                this.logger = logger;
                this.categoryName = categoryName;
            }


            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                var message = formatter(state, exception).Replace('\n', '\r');
                if (exception == null)
                {
                    logger.Write(logLevel, "SYSTEM_LOG", new
                    {
                        categoryName,
                        message,
                    });
                }
                else
                {
                    logger.Write(logLevel, "SYSTEM_LOG", new
                    {
                        categoryName,
                        message,
                        error = exception.Message,
                        stacktrace = exception.StackTrace
                    });
                }
            }
        }
    }
}
