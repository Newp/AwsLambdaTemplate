using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwsLambdaTemplate
{
    public class LogProvider : ILoggerProvider
    {
        public static bool Filter(LogLevel logLevel)
        {
            return logLevel > LogLevel.Warning;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new Logger() { categoryName = categoryName };
        }

        public void Dispose()
        {
        }

        class Logger : ILogger
        {

            public string categoryName { get; set; }

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
                var text = formatter(state, exception).Replace('\n', '\r');
                if (exception == null)
                {
                    Console.WriteLine($"[{categoryName}] [{logLevel.ToString()}] {text}");
                }
                else
                {
                    Console.WriteLine($"[{categoryName}] {text}\n" + exception.ToString());
                }
            }
        }
    }
}
