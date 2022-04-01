using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommandsService.Models.FluentModelMap;
public class CommandConfiguration : IEntityTypeConfiguration<Command>
{
    public void Configure(EntityTypeBuilder<Command> b)
    {
        b.HasKey(k => k.Id);
        b.Property(k => k.Id).ValueGeneratedOnAdd();
        b.Property(k => k.HowTo).IsRequired();
        b.Property(k => k.PlatformId).IsRequired();
        b.Property(k => k.CommandLine).IsRequired();
        b.HasOne(p => p.Platform)
        .WithMany(p => p.Commands)
        .HasForeignKey(p => p.PlatformId);
    }
}
