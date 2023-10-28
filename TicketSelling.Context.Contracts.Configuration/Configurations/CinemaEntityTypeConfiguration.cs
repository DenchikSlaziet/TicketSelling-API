using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Context.Contracts.Configuration.Configurations
{
    public class CinemaEntityTypeConfiguration : IEntityTypeConfiguration<Cinema>
    {
        void IEntityTypeConfiguration<Cinema>.Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => x.Title).HasDatabaseName($"{nameof(Cinema)}_{nameof(Cinema.Title)}");
            builder.Property(x => x.Address).HasMaxLength(100).IsRequired();
            builder.HasIndex(x => x.Address).HasDatabaseName($"{nameof(Cinema)}_{nameof(Cinema.Address)}").IsUnique();
            builder.HasMany(x => x.Tickets).WithOne(x => x.Cinema).HasForeignKey(x => x.CinemaId);
        }
    }
}
