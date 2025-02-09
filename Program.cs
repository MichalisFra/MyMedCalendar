using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using MyMedCalendar.Data;
using MyMedCalendar.Repositories;
using MyMedCalendar.Services;
using AutoMapper;
using MyMedCalendar.Configuration;
using MyMedCalendar.Models;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration)
          .WriteTo.Console()
          .WriteTo.File("Logs/logs.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 10);
});

// Database context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories and services
builder.Services.AddRepositories();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IMedicationService, MedicationService>();
builder.Services.AddScoped<IDrugService, DrugService>();


// Add ASP.NET Identity
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
})
.AddEntityFrameworkStores<AppDbContext>();

// Add AutoMapper
builder.Services.AddScoped(provider =>
    new MapperConfiguration(cfg =>
    {
        cfg.AddProfile(new MapperConfig());
    }).CreateMapper());

// Add Razor Pages and Controllers
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Configure Kestrel
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5000); // HTTP
    //serverOptions.ListenAnyIP(5001, listenOptions => listenOptions.UseHttps()); // HTTPS
    
});

try
{
    var app = builder.Build();

    // Middleware configuration
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
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

    // Migrate database and seed data
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<AppDbContext>();

            // Ensure the database is created


            // Apply Migrations if Pending
            if (!context.Database.CanConnect())
            {
                context.Database.Migrate();
            }

            // Seed Data if Needed
            SeedDatabase.Initialize(services);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while ensuring the database exists, migrating, or seeding data.");
            throw; // Re-throw to handle it in the outer try-catch
        }
    }


    app.MapControllers();
    app.MapRazorPages();

    app.Run();

}
catch (Exception ex)
{
    Console.WriteLine($"Application failed to start. Error: {ex.Message}");
    Console.WriteLine("Press Enter to exit...");
    Console.ReadLine(); // Wait for the user to press Enter before exiting
}
