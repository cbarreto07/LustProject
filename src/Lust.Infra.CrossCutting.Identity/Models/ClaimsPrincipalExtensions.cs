using AspNet.Security.OpenIdConnect.Primitives;

using System;
using System.Security.Claims;

namespace Lust.Infra.CrossCutting.Identity.Models
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            //var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            var claim = principal.FindFirst(OpenIdConnectConstants.Claims.Subject);
            return claim?.Value;
        }
    }
}
