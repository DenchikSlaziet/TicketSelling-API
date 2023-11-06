using TicketSelling.Common.Entity.EntityInterface;

namespace TicketSelling.Context.Contracts.Models
{
    /// <summary>
    /// Клиент
    /// </summary>
    public class Client : BaseAuditEntity, IEntity, IEntityWithId
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; } = string.Empty;

        /// <summary>
        /// Возраст
        /// </summary>
        public short Age { get; set; }

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string? Email { get; set; } = string.Empty;

        public ICollection<Ticket> Tickets { get; set; }
    }
}
