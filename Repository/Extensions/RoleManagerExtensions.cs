using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Extensions
{
    public static class RoleManagerExtensions
    {
        // adds "complete" CRUD permission, can later be separeted into C, R, U, D.
        public static async Task AddPermissionClaim(this RoleManager<IdentityRole<int>> roleManager, IdentityRole<int> role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissinos = roleManager.GeneratePermissionsForModule(module);

            foreach (var permission in allPermissinos)
            {
                if (!allClaims.Any(claim => claim.Type == "Permission" && claim.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }
        }

        public static List<string> GeneratePermissionsForModule(this RoleManager<IdentityRole<int>> roleManager, string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Read",
                $"Permissions.{module}.Update",
                $"Permissions.{module}.Delete"
            };
        }
    }
}
