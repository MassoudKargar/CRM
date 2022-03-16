namespace PlatformService.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
     : base(options)
    {
        Platforms = default!;
    }
    public DbSet<Platform> Platforms { get; set; }
}