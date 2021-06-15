using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ServiceStation.DAL.Models;

namespace ServiceStation.WebUI
{
    public class IdentityInitializer
    {
        private const string _email = "yuri.pekhov@gmail.com";
        private const string _password = "123456Aa_";
        private const string _surname = "Pekhov";
        private const string _firstName = "Yuri";

        public static async Task InitializeAsync(UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync(Roles.Admin) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }
            if (await roleManager.FindByNameAsync(Roles.User) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.User));
            }
            if (await userManager.FindByNameAsync(_email) == null)
            {
                var admin = new Employee { Surname = _surname, FirstName = _firstName, Email = _email, UserName = _email };
                IdentityResult result = await userManager.CreateAsync(admin, _password);
                
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, Roles.Admin);
                }
            }
        }
    }
}
