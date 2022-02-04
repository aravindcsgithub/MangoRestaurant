using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Mango.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Mango.Services.Identity.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public ProfileService(UserManager<ApplicationUser> _userManager,
            RoleManager<IdentityRole> _roleManager,
            IUserClaimsPrincipalFactory<ApplicationUser> __ClaimFactory)
        {
            userManager = _userManager;
            roleManager = _roleManager; 
            userClaimsPrincipalFactory = __ClaimFactory;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string sub = context.Subject.GetSubjectId();
            ApplicationUser user = await userManager.FindByIdAsync(sub);
            ClaimsPrincipal userClaims= await  userClaimsPrincipalFactory.CreateAsync(user);

            List<Claim> claims = userClaims.Claims.ToList(); 
            claims = claims.Where(claim=>context.RequestedClaimTypes.Contains(claim.Type)).ToList();
            claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
            claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));

            if (userManager.SupportsUserRole)
            {
                IList<string> roles = await userManager.GetRolesAsync(user);
                foreach(var role in roles)
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, role));
                    if(roleManager.SupportsRoleClaims)
                    {
                        IdentityRole identityRole = await roleManager.FindByNameAsync(role);
                        if(identityRole != null)
                        {
                            claims.AddRange(await roleManager.GetClaimsAsync(identityRole));
                        }
                    }
                }
            }

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            string sub = context.Subject.GetSubjectId();
            ApplicationUser user = await userManager.FindByIdAsync(sub);
            context.IsActive = user != null;

        }
    }
}
