global using TransportationManagement.Services;
global using TransportationManagement.Services.Impl;
global using TransportationManagement.Models;
global using System.ComponentModel.DataAnnotations.Schema;
global using System.ComponentModel.DataAnnotations;
global using TransportationManagement.Paging;

using Microsoft.EntityFrameworkCore;
using TransportationManagement.Data;
using TransportationManagement.Factories;
using TransportationManagement.Factories.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HumanResourceManagementDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HumanResourceManagementDBContext"),sqlServerOptionsAction: SqlOptions => { SqlOptions.EnableRetryOnFailure(); }));
//Inject Service classes 
builder.Services.AddTransient<ServiceFactory, ServiceFactoryImpl>();
builder.Services.AddTransient<DriverService, DriverServiceImpl>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

var app = builder.Build();
app.UseSession();
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}");

app.Run();
