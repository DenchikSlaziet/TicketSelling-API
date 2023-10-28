﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Context.Contracts.Configuration.Configurations
{
    public class TicketIEntityTypeConfiguration : IEntityTypeConfiguration<Ticket>
    {
        void IEntityTypeConfiguration<Ticket>.Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Date).IsRequired();
            builder.HasIndex(x => x.Date).HasDatabaseName($"{nameof(Ticket)}_{nameof(Ticket.Date)}");
            builder.Property(x => x.Place).HasMaxLength(2).IsRequired();
            builder.Property(x => x.Row).HasMaxLength(2).IsRequired();
            builder.Property(x => x.Price).HasMaxLength(5).IsRequired();
            builder.Property(x => x.StaffId).IsRequired();
            builder.Property(x => x.HallId).IsRequired();
            builder.Property(x => x.CinemaId).IsRequired();
            builder.Property(x => x.ClientId).IsRequired();
            builder.Property(x => x.FilmId).IsRequired();
        }
    }
}
