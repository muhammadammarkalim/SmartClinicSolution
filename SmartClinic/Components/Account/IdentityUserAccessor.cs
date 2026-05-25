using Microsoft.AspNetCore.Identity;
using SmartClinic.Models; // Added to find AppUser

namespace SmartClinic.Components.Account
{
    internal sealed class IdentityUserAccessor(UserManager<AppUser> userManager, IdentityRedirectManager redirectManager)
    {
        public async Task<AppUser> GetRequiredUserAsync(HttpContext context)
        {
            var user = await userManager.GetUserAsync(context.User);
            if (user is null)
            {
                redirectManager.RedirectToWithStatus("Account/Login", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
            }

            return user;
        }
    }
}