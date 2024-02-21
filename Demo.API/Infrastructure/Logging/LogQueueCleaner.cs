using Demo.API.Data;

namespace Demo.API.Infrastructure.Logging
{
    internal class LogQueueCleaner : IHostedService, IDisposable
    {
        private readonly ILogsQueue _logsQueue;
        private readonly IServiceProvider _services;
        private readonly TimeSpan _maxIdleTime;
        private readonly int _maxQueueCount;
        private Timer _timer;

        public LogQueueCleaner(ILogsQueue logsQueue, IServiceProvider services)
        {
            _logsQueue = logsQueue;
            _services = services;
            _maxIdleTime = TimeSpan.FromSeconds(10);
            _maxQueueCount = 30;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            DoWork(true);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer.Dispose();
        }

        private void DoWork(object state)
        {
            var isTimeDeserved = _logsQueue.IdleTime >= _maxIdleTime;
            var isFinishing = state is bool booleanState && booleanState;
            var isCountDeserved = _logsQueue.LogsCount >= _maxQueueCount;
            var hasLogs = _logsQueue.LogsCount > 0;
            if (hasLogs && (isTimeDeserved || isCountDeserved || isFinishing))
            {
                using (var scope = _services.CreateScope())
                {
                    var logEntries = _logsQueue.DequeueAllLogEntries();
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    dbContext.Logs.AddRange(logEntries);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
