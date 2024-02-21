using System.Security.Claims;

namespace Demo.API.Infrastructure.Auth
{
    public static class Extensions
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            return user.GetClaim(CustomClaimNames.UserId).Value;
        }

        public static string GetFirstName(this ClaimsPrincipal user)
        {
            return user.GetClaim(CustomClaimNames.GivenName).Value;
        }

        public static string GetLastName(this ClaimsPrincipal user)
        {
            return user.GetClaim(CustomClaimNames.FamilyName).Value;
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user.GetClaim(CustomClaimNames.Email).Value;
        }

        private static Claim GetClaim(this ClaimsPrincipal user, string claimType)
        {
            return user.Claims
                .Single(claim => claim.Type == claimType);
        }
    }
}
