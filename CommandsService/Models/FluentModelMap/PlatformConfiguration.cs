using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommandsService.Models.FluentModelMap;
public class PlatformConfiguration : IEntityTypeConfiguration<Platform>
{
    public void Configure(EntityTypeBuilder<Platform> b)
    {
        b.HasKey(k => k.Id);
        b.Property(k => k.Id).ValueGeneratedOnAdd();
        b.Property(k => k.Name).IsRequired();
        b.Property(k => k.ExternalID).IsRequired();
        b.HasMany(p => p.Commands)
        .WithOne(p => p.Platform!)
        .HasForeignKey(p => p.PlatformId);
    }
}