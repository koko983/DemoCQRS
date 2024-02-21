namespace Demo.API.Infrastructure.Logging
{
    [ProviderAlias("Custom")]
    internal class CustomLogProvider : ILoggerProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public CustomLogProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new CustomLogger(_serviceProvider, categoryName);
        }
    }
}
