using Demo.API.Domain.Entities.Logging;

namespace Demo.API.Infrastructure.Logging
{
    public interface ILogsQueue
    {
        int LogsCount { get; }

        TimeSpan IdleTime { get; }

        LogEntry[] DequeueAllLogEntries();

        void EnqueueLogEntry(LogEntry log);
    }
}
