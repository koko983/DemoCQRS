namespace Demo.API.Infrastructure.Auth
{
    public class RequestInfo
    {
        public string IpAddress { get; }

        public string BrowserInfo { get; }

        public string ConnectionId { get; }

        public string RequestedAction { get; }

        public string RequestUrl { get; }

        public RequestInfo(
            string ipAddress,
            string browserInfo,
            string connectionId,
            string requestedAction,
            string requestUrl)
        {
            IpAddress = ipAddress;
            BrowserInfo = browserInfo;
            ConnectionId = connectionId;
            RequestedAction = requestedAction;
            RequestUrl = requestUrl;
        }

        public static RequestInfo Empty()
        {
            return new RequestInfo(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }
    }
}
