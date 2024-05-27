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
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<HumanResourceManagementDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TransportationManagementDBContext"), sqlServerOptionsAction: SqlOptions => { SqlOptions.EnableRetryOnFailure(); }));

//Inject Service classes 
builder.Services.AddTransient<ServiceFactory, ServiceFactoryImpl>();
builder.Services.AddTransient<DriverService, DriverServiceImpl>();
builder.Services.AddTransient<YBSCompanyService, YBSCompanyServiceImpl>();
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

string GetConnectionString(IWebHostEnvironment environment, IConfiguration configuration )
{

    Console.WriteLine("Con str: " + configuration.GetSection("ConnectionStrings").GetSection("ServerConnection").Value);
        return configuration.GetSection("ConnectionStrings").GetSection("ServerConnection").Value;//$"Server={DEFAULT_DATABASE_NAME};Database={DEFAULT_SERVER};User Id={DEFAULT_USERID};Password={DEFAULT_PASSWORD};";
    
}
