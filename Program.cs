using Serilog;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Services;
using QuestPDF.Infrastructure; // 1. Namespace obligatorio de QuestPDF

// 2. Configuración obligatoria de la licencia (Comunitaria) para que no falle la generación del PDF
QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

// 3. Logging con Serilog
builder.Host.UseSerilog((ctx, cfg) => cfg
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("Logs/techstore-.log", rollingInterval: RollingInterval.Day));

// 4. Registrar DbContext e Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// 5. Registrar los servicios de los controladores (Inyección de dependencias)
builder.Services.AddScoped<ITechStoreService, TechStoreService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// 6. Configurar el pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Home/StatusCode", "?code={0}");
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// 7. Crear base de datos, aplicar migraciones y roles/usuarios iniciales automáticamente
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    await DbInitializer.SeedRolesAndUsersAsync(roleManager, userManager);
}

app.Run();

public partial class Program { }