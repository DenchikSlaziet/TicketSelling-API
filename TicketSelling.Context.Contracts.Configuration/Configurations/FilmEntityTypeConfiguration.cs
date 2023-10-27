using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Context.Contracts.Configuration.Configurations
{
    public class FilmEntityTypeConfiguration : IEntityTypeConfiguration<Film>
    {
        void IEntityTypeConfiguration<Film>.Configure(EntityTypeBuilder<Film> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()").IsRequired();
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => x.Title)
                .IsUnique()
                .HasDatabaseName(nameof(Film.Title));
            builder.Property(x => x.Description).HasMaxLength(100);
            builder.Property(x => x.Limitation).HasMaxLength(2).IsRequired();
            builder.HasMany(x => x.Tickets).WithOne(x => x.Film).HasForeignKey(x => x.FilmId);
        }
    }
}
