using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Context.Contracts.Configuration.Configurations
{
    public class StaffIEntityTypeConfiguration : IEntityTypeConfiguration<Staff>
    {
        void IEntityTypeConfiguration<Staff>.Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).
               ValueGeneratedOnAdd().
               UseIdentityColumn();
            builder.Property(x => x.Post).HasMaxLength(1).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(40).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Patronymic).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Age).HasMaxLength(2);
            builder.HasMany(x => x.Tickets).WithOne(x => x.Staff).HasForeignKey(x => x.StaffId);
        }
    }
}
