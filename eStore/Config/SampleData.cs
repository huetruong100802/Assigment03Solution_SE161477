using BusinessObject.Models;
using eStore.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Configuration;

namespace eStore.Config
{
    public class SampleData
    {
        private static IConfiguration _configuration;
        public static async Task<IdentityResult> SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _configuration = configuration;
            await SeedRoles(roleManager);
            await SeedUsers(userManager);

            return IdentityResult.Success;
        }
        private static async Task<IdentityResult> SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            var exists = await roleManager.RoleExistsAsync("Admin");
            if (!exists)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
        return IdentityResult.Success;
        }

        private static async Task<IdentityResult> SeedUsers(UserManager<ApplicationUser> userManager)
        {
            var account = _configuration.GetSection("DefaultAccount");
            var email = account.GetValue<string>("Email");
            var password = account.GetValue<string>("Password");
            var usr = await userManager.FindByEmailAsync(email);
            if (usr == null)
            {
                var user = new ApplicationUser
                {
                    Email = email,
                    NormalizedEmail = email.Normalize(),
                    UserName = email,
                    NormalizedUserName = email.Normalize(),
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                IdentityResult result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            return IdentityResult.Success;
        }

    }
}
