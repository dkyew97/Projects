using DDAC2.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDAC2
{
    public class Initializer
    {
        public static async Task Initialize(ApplicationDbContext context,
                             RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();


            string role1 = "Admin";


            string role2 = "Staff";

            if (await roleManager.FindByNameAsync(role1) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role1));
            }
            if (await roleManager.FindByNameAsync(role2) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role2));
            }
        }
    }
}
