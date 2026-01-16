using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.DataSeeding
{
    public static  class IdentityDbContextSeeding
    {
        public static bool SeedData(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            try
            {
                var HasUser = userManager.Users.Any();
                var HasRole = roleManager.Roles.Any();
                if (HasRole && HasUser) return false;
                if (!HasRole)
                {
                    var Roles = new List<IdentityRole>()
                    {
                        new () {Name="SuperAdmin"},
                        new () {Name="Admin"},


                    };
                    foreach (var role in Roles)
                    {
                        if (!roleManager.RoleExistsAsync(role.Name!).Result)
                        {
                            roleManager.CreateAsync(role).Wait();
                        }
                    }
                }

                if (!HasUser)
                {
                    var MainAdmin = new ApplicationUser()
                    {
                        FirstName = "Ahmed",
                        LastName = "Magdy",
                        UserName = "AhmedMagdy",
                        Email = "ahmedkandil55666@gmail.com",
                        PhoneNumber = "01227840651"

                    };
                    userManager.CreateAsync(MainAdmin, "P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(MainAdmin, "SuperAdmin").Wait();
                    var Admin = new ApplicationUser()
                    {
                        FirstName = "Ali",
                        LastName = "Kandil",
                        UserName = "AliKandil",
                        Email = "ali@gmail.com",
                        PhoneNumber = "01278613686"

                    };
                    userManager.CreateAsync(Admin, "P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(Admin, "Admin").Wait();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seed Faild {ex}");
                return false;
            }
        }
    }
}
