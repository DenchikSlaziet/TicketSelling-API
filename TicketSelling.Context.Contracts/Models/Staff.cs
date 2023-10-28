using TicketSelling.Context.Contracts.Enums;
using TicketSelling.Context.Contracts.Interfaces;

namespace TicketSelling.Context.Contracts.Models
{
    /// <summary>
    /// Кассир
    /// </summary>
    public class Staff : Person, IEntity
    {
        /// <summary>
        /// Должность
        /// </summary>
        public Post Post { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
