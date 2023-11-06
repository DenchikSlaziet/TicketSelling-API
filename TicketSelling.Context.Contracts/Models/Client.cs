using TicketSelling.Common.Entity.EntityInterface;

namespace TicketSelling.Context.Contracts.Models
{
    /// <summary>
    /// Клиент
    /// </summary>
    public class Client : Person, IEntity, IEntityWithId
    {
        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string? Email { get; set; } = string.Empty;

        public ICollection<Ticket> Tickets { get; set; }
    }
}
