using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Common.Entity.EntityInterface;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Context.Contracts.Configuration
{
    static internal class EntityTypeBuilderExtensions
    {
        /// <summary>
        /// Задаёт конфигурацию свойств аудита для сущности <inheritdoc cref="BaseAuditEntity"/>
        /// </summary>
        public static void PropertyAuditConfiguration<T>(this EntityTypeBuilder<T> builder)
            where T : BaseAuditEntity
        {
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(200);
            builder.Property(x => x.UpdatedAt).IsRequired();
            builder.Property(x => x.UpdatedBy).IsRequired().HasMaxLength(200);
        }
    }
}
