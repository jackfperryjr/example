using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Example.Models;

namespace Example.Data
{
    public static class ApplicationRole
    {
        public static void CreateRoles(IServiceProvider serviceProvider, IConfiguration Configuration)
        {
            const string adminRoleName = "Admin";
            string[] roleNames = { adminRoleName, "SuperUser", "User" };

            foreach (string roleName in roleNames)
            {
                CreateRole(serviceProvider, roleName);
            }
        }

        private static void CreateRole(IServiceProvider serviceProvider, string roleName)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            Task<bool> roleExists = roleManager.RoleExistsAsync(roleName);
            roleExists.Wait();

            if (!roleExists.Result)
            {
                Task<IdentityResult> roleResult = roleManager.CreateAsync(new IdentityRole(roleName));
                roleResult.Wait();
            }
        }
        
        private static void AddUserToRole(IServiceProvider serviceProvider, string userEmail, string userPwd, string roleName)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            Task<ApplicationUser> checkAppUser = userManager.FindByEmailAsync(userEmail);
            checkAppUser.Wait();

            ApplicationUser appUser = checkAppUser.Result;

            if (checkAppUser.Result == null)
            {
                ApplicationUser newUser = new ApplicationUser
                {
                    Email = userEmail,
                    UserName = userEmail
                };

                Task<IdentityResult> taskCreateUser = userManager.CreateAsync(newUser, userPwd);
                taskCreateUser.Wait();

                if (taskCreateUser.Result.Succeeded)
                {
                    appUser = newUser;
                }
            }

            Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(appUser, roleName);
            newUserRole.Wait();
        }
    }
}