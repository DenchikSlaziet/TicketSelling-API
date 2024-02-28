using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Context.Contracts.Configuration.Configurations
{
    public class TicketIEntityTypeConfiguration : IEntityTypeConfiguration<Ticket>
    {
        /// <summary>
        /// Конфигурация для <see cref="Ticket"/>
        /// </summary>
        void IEntityTypeConfiguration<Ticket>.Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.DatePayment).IsRequired();
            builder.Property(x => x.Place).IsRequired();
            builder.Property(x => x.Row).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.SessionId).IsRequired();
        }
    }
}
