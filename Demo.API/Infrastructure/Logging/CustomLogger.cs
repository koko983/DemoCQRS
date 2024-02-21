using Demo.API.Application.Services;
using Demo.API.Data;
using Demo.API.Domain.Entities.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Demo.API.Infrastructure.Logging
{
    public class CustomLogger : ILogger
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly string _categoryName;
        //private readonly Func<string, LogLevel, bool> _filter;

        public CustomLogger(
            IServiceProvider serviceProvider,
            string categoryName)
        {
            _serviceProvider = serviceProvider;
            _categoryName = categoryName;
            //_filter = filter;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);
            if (string.IsNullOrEmpty(message) && exception is null)
            {
                return;
            }
            using (var scope = _serviceProvider.CreateScope())
            {
                var userSession = scope.ServiceProvider.GetService<ICurrentUserIdentity>();
                var logsQueue = scope.ServiceProvider.GetService<ILogsQueue>();
                if (userSession is null || logsQueue is null) // not registered yet
                {
                    return;
                }
                var log = new LogEntry
                {
                    EventId = eventId.ToString(),
                    Level = logLevel.ToString(),
                    Exception = JsonConvert.SerializeObject(exception, Formatting.Indented),
                    Message = message,
                    Category = _categoryName,
                    Date = DateTime.UtcNow,
                    RequestedAction = userSession.Request.RequestedAction,
                    RequestUrl = userSession.Request.RequestUrl,
                    BrowserInfo = userSession.Request.BrowserInfo,
                    Email = userSession.User.Email,
                };
                logsQueue.EnqueueLogEntry(log);
            }
        }
    }
}
