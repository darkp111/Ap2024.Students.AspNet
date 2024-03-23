using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using Students.Common.Data;
using Students.Interfaces;
using Students.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ISharedResourcesService, SharedResourcesService>();
builder.Services.AddScoped<IDatabaseService, DatabaseService>();

// NLog: setup the logger first to catch all errors
LogManager.Setup().LoadConfigurationFromAppSettings();
var logger = LogManager.GetCurrentClassLogger();
try
{
    logger.Debug("init main");
    builder.Services.AddDbContext<StudentsContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("StudentsContext") ?? throw new InvalidOperationException("Connection string 'StudentsContext' not found.")));

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

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception ex)
{
    //NLog: catch setup errors
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}