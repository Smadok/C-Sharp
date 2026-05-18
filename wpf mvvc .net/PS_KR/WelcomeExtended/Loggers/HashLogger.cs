using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WelcomeExtended.Loggers
{
    class HashLogger:ILogger
    {
        private readonly ConcurrentDictionary<int,string> _logMessages
            =new();
        private readonly string _name;
        public HashLogger(string name)
        {
            _name = name;
        }
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }
        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            var message = formatter(state, exception);

            switch (logLevel)
            {
                case LogLevel.Critical:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case LogLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            var messageToBeLogged = new StringBuilder();
            messageToBeLogged.Append($"[{DateTime.Now}]");
            messageToBeLogged.AppendFormat(" [{0}]", _name);
            messageToBeLogged.AppendLine($" - {message}");

            Console.WriteLine(messageToBeLogged);
            Console.ResetColor();

            _logMessages.TryAdd(eventId.Id, message);
        }
    }
}
