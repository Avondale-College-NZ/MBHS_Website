using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MBHS_Website.Areas.Identity.Data;
using MBHS_Website.Models;

namespace MBHS_Website
{
    internal class Program
    {
        private static async Task Main(string[] args)
        { 
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("MBHS_ContextConnection") ?? throw new InvalidOperationException("Connection string 'MBHS_ContextConnection' not found.");

            builder.Services.AddDbContext<MBHS_Context>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<Teacher>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>().AddEntityFrameworkStores<MBHS_Context>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

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

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


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
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Teacher>>();

                string FirstName = "Admin";
                string LastName = "MBHS";
                DateTime DateOfBirth = new DateTime(2000, 1, 1);
                string email = "principal@mbhs.com";
                string password = "Passw0rd!";

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new Teacher();
                    user.UserName = email;
                    user.Email = email;
                    user.FirstName = FirstName;
                    user.LastName = LastName;
                    user.DateOfBirth = DateOfBirth;

                    await userManager.CreateAsync(user, password);

                    await userManager.AddToRoleAsync(user, "Admin");
                    await userManager.AddToRoleAsync(user, "Manager");
                        await userManager.AddToRoleAsync(user, "User");
                }

            }

            app.Run();
        }
    }
}