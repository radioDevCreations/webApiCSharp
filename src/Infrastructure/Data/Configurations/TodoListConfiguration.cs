using FinalLabProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalLabProject.Infrastructure.Data.Configurations;

public class HarbourConfiguration : IEntityTypeConfiguration<Harbour>
{
    public void Configure(EntityTypeBuilder<Harbour> builder)
    {
        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .OwnsOne(b => b.Colour);
    }
}
