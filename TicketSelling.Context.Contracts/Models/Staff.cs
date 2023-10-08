using TicketSelling.Context.Contracts.Enums;

namespace TicketSelling.Context.Contracts.Models
{
    /// <summary>
    /// Кассир
    /// </summary>
    public class Staff : Person
    {
        /// <summary>
        /// Должность
        /// </summary>
        public Post Post { get; set; }
    }
}
