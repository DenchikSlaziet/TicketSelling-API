using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Context.Contracts.Configuration.Configurations
{
    /// <summary>
    /// Конфигурация для <see cref="User"/>
    /// </summary>
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Role).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(40).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Patronymic).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(100);
            builder.Property(x => x.Login).HasMaxLength(100).IsRequired();
            builder.HasIndex(x => x.Login)
                .IsUnique()
                .HasDatabaseName($"{nameof(User)}_{nameof(User.Login)}")
                .HasFilter($"{nameof(User.DeletedAt)} is null");
            builder.Property(x => x.Password).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Age).IsRequired();
            builder.HasMany(x => x.Tickets).WithOne(x => x.User).HasForeignKey(x => x.UserId);
        }
    }
}
