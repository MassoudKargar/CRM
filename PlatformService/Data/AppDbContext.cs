
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace PlatformService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
     : base(options)
    {
    }
    public DbSet<Platform> Platforms { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);
        b.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());//Auto Map FluentApi Models for Assembly Library 
        b.HasDefaultSchema("dbo");
        b.ApplyFluentApi();
    }
}