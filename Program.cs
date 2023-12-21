using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Dishcover.Areas.Identity.Data;

namespace Dishcover
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("ApplicationDBContextConnection") ??
                                   throw new InvalidOperationException("Connection string 'ApplicationDBContextConnection' not found.");

            builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(connectionString));

            builder.Services
                .AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDBContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                ApplicationDBContext context =
                    new ApplicationDBContext(services.GetRequiredService<DbContextOptions<ApplicationDBContext>>());
                ApplicationDBContext.DataInitializer(context);
            }

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new[] { "Admin", "Manager", "User" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create a user for each role

            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                string email = "admin@neatbox.com";
                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new ApplicationUser() { FirstName = "Super", LastName = "User", UserName = email, Email = email };

                    await userManager.CreateAsync(user, "!Admin123");
                    await userManager.AddToRoleAsync(user, "Admin");
                }

                email = "sethye@neatbox.com";
                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new ApplicationUser() { FirstName = "Synthe", LastName = "Wineplace", UserName = email, Email = email };

                    await userManager.CreateAsync(user, "!Usr100");

                    await userManager.AddToRoleAsync(user, "Manager");
                }

                email = "cjanade@neatbox.com";
                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user3 = new ApplicationUser() { FirstName = "Steep", LastName = "Mountains", UserName = email, Email = email };

                    await userManager.CreateAsync(user3, "!Usr100");
                   
                    await userManager.AddToRoleAsync(user3, "User");
                }
            }

            app.Run();
        }
    }
}