﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Context.Contracts.Configuration.Configurations
{
    /// <summary>
    /// Конфигурация для <see cref="Staff"/>
    /// </summary>
    public class StaffIEntityTypeConfiguration : IEntityTypeConfiguration<Staff>
    {
        void IEntityTypeConfiguration<Staff>.Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.ToTable("Staffs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Post).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(40).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Patronymic).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Age).IsRequired();
            builder.HasMany(x => x.Tickets).WithOne(x => x.Staff).HasForeignKey(x => x.StaffId);
        }
    }
}
