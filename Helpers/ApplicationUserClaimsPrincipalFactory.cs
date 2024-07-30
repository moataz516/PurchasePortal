using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PurchasePortal.Web.Models;
using System.Security.Claims;

namespace PurchasePortal.Web.Helpers
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager,
RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options) { }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("Email", user.Email ?? ""));
            identity.AddClaim(new Claim("FirstName", user.FirstName ?? ""));
            //identity.AddClaim(new Claim("LastName", user.LastName ?? ""));
            identity.AddClaim(new Claim("UserId", user.Id ?? ""));
            //identity.AddClaim(new Claim("Image", user?.Image ?? "https://icon-library.com/images/no-user-image-icon/no-user-image-icon-3.jpg"));
            return identity;

        }
    }
}
