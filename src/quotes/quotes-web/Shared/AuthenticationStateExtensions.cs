using System.Security.Claims;

namespace quotes_web.Shared
{
    internal static class AuthenticationStateExtensions
    {
        public static bool IsAdmin(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.HasClaim("groups", "quotes-admin");
        }
    }
}
