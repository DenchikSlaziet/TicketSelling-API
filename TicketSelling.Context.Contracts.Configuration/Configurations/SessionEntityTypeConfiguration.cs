using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Context.Contracts.Configuration.Configurations
{
    /// <summary>
    /// Конфигурация для <see cref="Session"/>
    /// </summary>
    public class SessionEntityTypeConfiguration : IEntityTypeConfiguration<Session>
    {
        void IEntityTypeConfiguration<Session>.Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("Sessions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.StartDateTime).IsRequired();
            builder.Property(x => x.EndDateTime).IsRequired();
            builder.Property(x => x.HallId).IsRequired();
            builder.Property(x => x.FilmId).IsRequired();
            builder.HasMany(x => x.Tickets).WithOne(x => x.Session).HasForeignKey(x => x.SessionId);
        }
    }
}
