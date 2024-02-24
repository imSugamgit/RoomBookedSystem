using Microsoft.AspNetCore.Identity;
using RoomMainSetup.Data;
using RoomUser.Src.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RoomUser.Seeds
{
    public static class ClaimsHelper
    {
        public static void GetPermissions(this List<RoleClaimsViewModel> allPermission, Type policy, string roleId)
        {
            FieldInfo[] fields = policy.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo fi in fields)
            {
                allPermission.Add(new RoleClaimsViewModel { Value = fi.GetValue(null).ToString(), Type = "Permissions" });
            }
        }
        public static async Task AddPermissionClaims(this RoleManager<IdentityRole> roleManager, IdentityRole role, string permissions)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            if (!allClaims.Any(a => a.Type == "Permissions" && a.Value == permissions))
            {
                await roleManager.AddClaimAsync(role, new Claim("Permissions", permissions));
            }
        }
        public static async Task AddPermissionClaimsForUser(this UserManager<ApplicationUser> userManager, ApplicationUser user, string permissions)
        {
            var allClaims = await userManager.GetClaimsAsync(user);
            if (!allClaims.Any(a => a.Type == "Permissions" && a.Value == permissions))
            {
                await userManager.AddClaimAsync(user, new Claim("Permissions", permissions));
            }
        }
    }
}
