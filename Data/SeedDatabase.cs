using Microsoft.Extensions.DependencyInjection;
using MyMedCalendar.Data;
using MyMedCalendar.Models;

public static class SeedDatabase
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = serviceProvider.GetRequiredService<AppDbContext>();

        // Check if the database already has data to avoid duplicates
        if (!context.Drugs.Any())
        {
            context.Drugs.AddRange(
                new Drug { Name = "ABILIFY", Company = "Company A", PackageSize = 30 },
                new Drug { Name = "XANAX", Company = "Company B", PackageSize = 20 },
                new Drug { Name = "PANADOL", Company = "Company C", PackageSize = 10 }
            );

            context.SaveChanges();
        }
    }
}
