using System;
using System.Collections.Concurrent;
using System.Text;

namespace Utilities
{
    public enum LogLevel
    {
        Debug,
        Verbose,
        Info,
        Error,
        Critical
    }

    public enum LogOutput
    {
        Console,
    }

    public interface ILogger
    {
        void Log(string message);
        void Log(Exception exception);
        void Log(LogLevel level, string message);
        Log[] GetLogs();
    }

    public class Logger : ILogger
    {
        private readonly ConcurrentQueue<Log> _logs = new ConcurrentQueue<Log>();
        private readonly Type _parentType;
        private readonly LogOutput _logOutput;

        public Logger(Type parentType, LogOutput logOutput)
        {
            _parentType = parentType ?? GetType();
            _logOutput = logOutput;
        }

        public Logger(LogOutput logOutput)
        {
            _logOutput = logOutput;
        }

        public void Log(string message)
        {
            var log = new Log(DateTime.Now, _parentType, LogLevel.Info, message);
            StoreAndPrint(log);
        }

        public void Log(Exception exception)
        {
            var log = new Log(DateTime.Now, _parentType, LogLevel.Error, exception);
            StoreAndPrint(log);
        }

        public void Log(LogLevel level, string message)
        {
            var log = new Log(DateTime.Now, _parentType, level, message);
            StoreAndPrint(log);
        }

        public Log[] GetLogs()
        {
            return _logs.ToArray();
        }

        private void StoreAndPrint(Log log)
        {
            StoreLog(log);
            Print(log);
        }

        private void Print(Log log)
        {
            switch (_logOutput)
            {
                case LogOutput.Console:
                    Console.WriteLine(log);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void StoreLog(Log log)
        {
            _logs.Enqueue(log);
        }
    }

    public class Log
    {
        internal DateTime Timestamp { get; }
        internal Type ParentType { get; }
        internal LogLevel Level { get; }
        internal string Message { get; }
        internal Exception Exception { get; }

        public Log(DateTime timestamp, Type parentType, LogLevel level, string message)
        {
            Timestamp = timestamp;
            ParentType = parentType;
            Level = level;
            Message = message;
        }

        public Log(DateTime timestamp, Type parentType, LogLevel level, Exception exception)
        {
            Timestamp = timestamp;
            ParentType = parentType;
            Level = level;
            Message = exception.Message;
            Exception = exception;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            var m = 
                Exception != null
                ? $"{Message} - Stacktrace: {Exception.StackTrace}"
                : Message;

            sb.Append($"[{Timestamp}] ({Level}) - {ParentType}: {m}");

            return sb.ToString();
        }
    }
}
