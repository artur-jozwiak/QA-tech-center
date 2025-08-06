using Microsoft.AspNetCore.Identity;
using QA.DataAccess;

namespace QA.UI.Initial
{
    public static class AdminAccountInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider
                .GetRequiredService<QAContext>();
            context.Database.EnsureCreated();
            var roleManager = serviceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();
            var roleName = "Admin";
            IdentityResult result;
            var roleExist = await roleManager.RoleExistsAsync(roleName);

            if (!roleExist)
            {
                result = await roleManager
                    .CreateAsync(new IdentityRole(roleName));
                if (result.Succeeded)
                {
                    var userManager = serviceProvider
                        .GetRequiredService<UserManager<IdentityUser>>();
                    var config = serviceProvider
                        .GetRequiredService<IConfiguration>();
                    var admin = await userManager
                        .FindByNameAsync(config["AdminCredentials:UserName"]);

                    if (admin == null)
                    {
                        admin = new IdentityUser()
                        {
                            UserName = config["AdminCredentials:UserName"],
                            Email = config["AdminCredentials:Email"],
                            EmailConfirmed = false
                        };

                        result = await userManager
                            .CreateAsync(admin, config["AdminCredentials:Password"]);

                        if (result.Succeeded)
                        {
                            result = await userManager
                                .AddToRoleAsync(admin, roleName);
                            if (!result.Succeeded)
                            {
                                // todo: process errors
                            }
                        }
                    }
                }
            }
        }
    }
}
