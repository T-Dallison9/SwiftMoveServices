using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SwiftMoveServices.Data;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

//Run role + user setup inside this async method
await CreateRolesAndDefaultUserAsync(app);

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();


//Async setup method
static async Task CreateRolesAndDefaultUserAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string[] roles = { "Customer", "Staff" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
            Console.WriteLine($"Role {role} created.");
        }
    }

    
    var defaultUserEmail = "tylerd2506@gmail.com";
    var defaultUserPassword = "RootPass123!!??";

    var user = await userManager.FindByEmailAsync(defaultUserEmail);
    if (user == null)
    {
        var newUser = new IdentityUser
        {
            UserName = defaultUserEmail,
            Email = defaultUserEmail,
            EmailConfirmed = true
        };

        var createResult = await userManager.CreateAsync(newUser, defaultUserPassword);

        if (createResult.Succeeded)
        {
            Console.WriteLine("Default user created successfully.");
            await userManager.AddToRoleAsync(newUser, "Staff");
        }
        else
        {
            Console.WriteLine("Failed to create default user:");
            foreach (var error in createResult.Errors)
            {
                Console.WriteLine($" - {error.Description}");
            }
        }
    }
    else
    {
        Console.WriteLine("Default user already exists.");
    }
}
