using System;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;

namespace FullStack.Core.Extensions
{
    public static class IPrincipalExtensions
    {
        #region Extension Methods

        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string GetUserName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static string GetEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.Email)?.Value;
        }

        public static IEnumerable<string> GetRoles(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindAll(ClaimTypes.Role)?.Select(x => x.Value);
        }

        public static bool IsInRole(this ClaimsPrincipal principal, params string[] roles)
        {
            foreach (var role in roles)
            {
                if (principal.IsInRole(role))
                    return true;
            }

            return false;
        }

        #endregion
    }
}
