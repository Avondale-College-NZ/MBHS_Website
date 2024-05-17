using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MBHS_Website.Areas.Identity.Data;
using MBHS_Website.Models;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MBHS_ContextConnection") ?? throw new InvalidOperationException("Connection string 'MBHS_ContextConnection' not found.");

builder.Services.AddDbContext<MBHS_Context>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<Teacher>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MBHS_Context>();

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

app.Run();
