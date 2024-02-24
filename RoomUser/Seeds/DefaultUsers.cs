using Microsoft.AspNetCore.Identity;
using RoomMainSetup.Data;
using RoomUser.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RoomUser.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedBasicUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "ejamsugam@gmail.com",
                EmailConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "superadmin");
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.User.ToString());
                }
            }
        }
        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "ejamsugam@gmail.com",
                EmailConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "superadmin");
                    await userManager.AddToRoleAsync(defaultUser, Roles.User.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                }
                await roleManager.SeedClaimsForSuperAdmin();
            }
        }
        private async static Task SeedClaimsForSuperAdmin(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("SuperAdmin");
            await roleManager.AddPermissionClaim(adminRole, "UserRoles");
            await roleManager.AddPermissionClaim(adminRole, "Roles");
            await roleManager.AddPermissionClaim(adminRole, "District");
            await roleManager.AddPermissionClaim(adminRole, "Provience");
            await roleManager.AddPermissionClaim(adminRole, "LocalLevel");
        }
        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermission = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermission)
            {
                if (!allClaims.Any(a => a.Type == "Permissions" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permissions", permission));
                }
            }
        }
    }
}
