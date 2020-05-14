using System;
using System.Linq;
using System.Threading.Tasks;
using PVE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PVE.Data;

namespace PVE
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            try
            {
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await EnsureRolesAsync(roleManager);

                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                await EnsureTestAdminAsync(userManager);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var alreadyExists = await roleManager
                .RoleExistsAsync(Constants.AdministratorRole);

            if (alreadyExists) return;

            await roleManager.CreateAsync(
                new IdentityRole(Constants.AdministratorRole));
        }

        private static async Task EnsureTestAdminAsync(UserManager<IdentityUser> userManager)
        {
            var testAdmin = await userManager.Users
                .Where(x => x.UserName == Constants.AdminUserName)
                .SingleOrDefaultAsync();

            if (testAdmin != null) return;

            testAdmin = new IdentityUser
            {
                UserName = Constants.AdminUserName,
                Email = Constants.AdminUserEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(testAdmin, "Ybsx#123");
            await userManager.AddToRoleAsync(testAdmin, Constants.AdministratorRole);
        }


    }
}