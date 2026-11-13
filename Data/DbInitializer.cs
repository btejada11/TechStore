using Microsoft.AspNetCore.Identity;

namespace TechStore.Data
{
    public static class DbInitializer
    {
        public static async Task SeedRolesAndUsersAsync(
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager)
        {
            // Creación de roles base
            string[] roles = ["Admin", "Cliente"];
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Cuenta Administrador inicial
            var adminEmail = "admin@techstore.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123*");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

           
        }
    }
}