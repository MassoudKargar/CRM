using System.Xml;
namespace CommandsService.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Commands = null!;
        platforms = null!;
    }

    public DbSet<Command> Commands { get; set; }
    public DbSet<Platform> platforms { get; set; }

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);
        b.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());//Auto Map FluentApi Models for Assembly Library 
        b.HasDefaultSchema("dbo");
        b.ApplyFluentApi();
    }
}
