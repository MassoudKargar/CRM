namespace PlatformService.Data;
public static class PrepDb
{
    public static void PrepPopulation(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            SeedData(context: serviceScope.ServiceProvider.GetRequiredService<AppDbContext>());
        }

    }
    private static void SeedData(AppDbContext context)
    {
        if (context is null)
        {
            return;
        }
        if (!context.Platforms.Any())
        {
            Console.WriteLine("--> Seeding Data...");
            context.Platforms.AddRange(
                new Platform
                {
                    Name = "Dot Net",
                    Publisher = "microsoft",
                    Cost = "Free"
                },
                new Platform
                {
                    Name = "SQL Server Express",
                    Publisher = "Microsoft",
                    Cost = "Free"
                },
                new Platform
                {
                    Name = "Kubernetes",
                    Publisher = "Cloud Nitave Computing Foundation",
                    Cost = "Free"
                }
            );
            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("--> We already have data");
        }
    }
}