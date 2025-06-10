using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Infrastructure._Identity
{
    public static class ApplicationIdentityContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any()) 
            {
                var user = new ApplicationUser()
                {
                    DisplayName = "youssef samir",
                    Email = "youssef.samir@gmail.com",
                    UserName = "youssef.samir",
                    PhoneNumber = "01202203469",
                    
                }; 
           
                await userManager.CreateAsync(user, "P@ssw0rd");

            }
        }
    }
}
