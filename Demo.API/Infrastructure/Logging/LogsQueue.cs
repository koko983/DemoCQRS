using Demo.API.Domain.Entities.Logging;
using System.Collections.Concurrent;

namespace Demo.API.Infrastructure.Logging
{
    public class LogsQueue : ILogsQueue
    {
        private readonly ConcurrentQueue<LogEntry> _logs = new ConcurrentQueue<LogEntry>();
        private DateTime _lastActivity = DateTime.UtcNow;

        public int LogsCount => _logs.Count;

        public TimeSpan IdleTime => DateTime.UtcNow - _lastActivity;

        public LogEntry[] DequeueAllLogEntries()
        {
            var result = new List<LogEntry>();
            while (_logs.TryDequeue(out var log))
            {
                result.Add(log);
            }

            return result.ToArray();
        }

        public void EnqueueLogEntry(LogEntry log)
        {
            _lastActivity = DateTime.UtcNow;
            _logs.Enqueue(log);
        }
    }
}
