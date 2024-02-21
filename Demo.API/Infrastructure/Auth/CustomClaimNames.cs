using System.Security.Claims;

namespace Demo.API.Infrastructure.Auth
{
    public class CustomClaimNames
    {
        public const string UserId = "user_id";
        public const string Email = ClaimTypes.Email;
        public const string UserName = ClaimTypes.Name;
        public const string GivenName = ClaimTypes.GivenName;
        public const string FamilyName = ClaimTypes.Surname;
    }
}
