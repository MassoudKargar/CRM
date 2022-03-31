namespace PlatformService.Data;
public static class PrepDb
{
    public static void PrepPopulation(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        using (IServiceScope serviceScope = app.ApplicationServices.CreateScope())
        {
            SeedData(context: serviceScope?.ServiceProvider.GetService<AppDbContext>(), env: env);
        }

    }
    private static void SeedData(AppDbContext? context, IWebHostEnvironment env)
    {
        if (env.IsProduction())
        {
            if (context is null) return;
            try
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not run migrations:{ex.Message}");
            }
        }
        else
        {
            if (context is null) return;
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
}