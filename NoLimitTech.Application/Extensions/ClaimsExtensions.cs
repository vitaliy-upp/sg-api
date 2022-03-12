using System.Security.Claims;

namespace NoLimitTech.Application.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetValueByClaimType(this ClaimsIdentity claim, string claimType)
        {
            if (claim.HasClaim(x => x.Type == claimType))
            {
                return claim.FindFirst(x => x.Type == claimType).Value;
            }
            return "";
        }
    }
}
