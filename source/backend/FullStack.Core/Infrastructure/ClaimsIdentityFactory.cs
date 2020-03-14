using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FullStack.Domain.Entities;
using FullStack.Domain.Constants;
using Microsoft.Extensions.Options;
using FullStack.Domain.Enumerations;
using Microsoft.AspNetCore.Identity;

namespace FullStack.Core.Infrastructure
{
    public class ClaimsIdentityFactory : UserClaimsPrincipalFactory<AppUser>
    {
        #region Constructors

        public ClaimsIdentityFactory(UserManager<AppUser> userManager, IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor)
        { }

        #endregion

        #region Overriden Methods

        public async override Task<ClaimsPrincipal> CreateAsync(AppUser user)
        {
            var principal = await base.CreateAsync(user);
            var roles = await UserManager.GetRolesAsync(user);

            ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.Email, user.Email));
            ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.Name, user.UserName));

            foreach (var role in roles)
            {
                ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.Role, role));

                if (Enum.TryParse(typeof(EnumRoles), role, true, out var item))
                    ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(IdentityServerApiConstants.StandardClaimsConstants.RoleId, ((int)(EnumRoles)item).ToString()));
            }

            return principal;
        }

        #endregion
    }
}
