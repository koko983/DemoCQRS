namespace Demo.API.Domain.Entities.Logging
{
    public class LogEntry : Entity
    {
        public string EventId { get; set; }

        public string Category { get; set; }

        public string Level { get; set; }

        public string Exception { get; set; }

        public string Message { get; set; }

        public string BrowserInfo { get; set; }

        public string RequestedAction { get; set; }

        public string RequestUrl { get; set; }

        public DateTime Date { get; set; }

        public string Email { get; set; }
    }
}
