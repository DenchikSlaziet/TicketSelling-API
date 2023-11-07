using TicketSelling.Common.Entity.EntityInterface;

namespace TicketSelling.Context.Contracts.Models
{
    /// <summary>
    /// Фильм
    /// </summary>
    public class Film : BaseAuditEntity, IEntity, IEntityWithId
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Ограничение по возросту
        /// </summary>
        public short Limitation { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
