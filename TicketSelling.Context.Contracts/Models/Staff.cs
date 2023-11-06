using TicketSelling.Context.Contracts.Enums;
using TicketSelling.Common.Entity.EntityInterface;

namespace TicketSelling.Context.Contracts.Models
{
    /// <summary>
    /// Кассир
    /// </summary>
    public class Staff : Person, IEntity, IEntityWithId
    {
        /// <summary>
        /// Должность
        /// </summary>
        public Post Post { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
