using Demo.API.Infrastructure.Auth;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;

namespace Demo.API.Application.Services
{
    public interface ICurrentUserIdentity
    {
        UserInfo User { get; }

        /// <summary>
        /// Current request information
        /// </summary>
        RequestInfo Request { get; }
    }

    internal class CurrentUserIdentity : ICurrentUserIdentity
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private HttpContext HttpContext => _httpContextAccessor.HttpContext;
        public CurrentUserIdentity(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserInfo User
        {
            get
            {
                if (HttpContext is null)
                {
                    return UserInfo.ForBackgroundJob();
                }
                var httpUser = HttpContext.User;
                return httpUser.Identity.IsAuthenticated
                    ? CreateUserInfo(httpUser)
                    : UserInfo.ForAnonymousUser();
            }
        }

        public RequestInfo Request => HttpContext is null
            ? RequestInfo.Empty()
            : CreateRequestInfo(HttpContext);

        private UserInfo CreateUserInfo(ClaimsPrincipal user) => new UserInfo(
            id: user.GetId(),
            name: user.GetFirstName() + " " + user.GetLastName(),
            email: user.GetEmail());

        private RequestInfo CreateRequestInfo(HttpContext httpContext)
        {
            var controller = httpContext.GetRouteValue("controller");
            var action = httpContext.GetRouteValue("action");
            return new RequestInfo(
                ipAddress: httpContext.Connection.RemoteIpAddress.ToString(),
                browserInfo: httpContext.Request.Headers[HeaderNames.UserAgent],
                connectionId: httpContext.TraceIdentifier,
                requestedAction: $"{controller}::{action}",
                requestUrl: httpContext.Request.GetDisplayUrl());
        }
    }
}
