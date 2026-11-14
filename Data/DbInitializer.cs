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
            string[] roles = ["Admin","Vendedor","Cliente"];
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
            var vendedorEmail = "vendedor@techstore.com";
            if (await userManager.FindByEmailAsync(vendedorEmail) == null)
            {
                var vendedorUser = new IdentityUser
                {
                    UserName = vendedorEmail,
                    Email = vendedorEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(vendedorUser, "Vendedor123*");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(vendedorUser, "Vendedor");
                }
            }
            var clienteEmail = "cliente@techstore.com";
            if (await userManager.FindByEmailAsync(clienteEmail) == null)
            {
                var clienteUser = new IdentityUser
                {
                    UserName = clienteEmail,
                    Email = clienteEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(clienteUser, "Cliente123*");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(clienteUser, "Cliente");
                }
            }

        }
    }
}