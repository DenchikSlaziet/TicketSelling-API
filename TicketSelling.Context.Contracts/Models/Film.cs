using TicketSelling.Context.Contracts.Enums;

namespace TicketSelling.Context.Contracts.Models
{
    /// <summary>
    /// Фильм
    /// </summary>
    public class Film : BaseAuditEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Ограничение по возросту
        /// </summary>
        public int Limitation { get; set; }

        /// <summary>
        /// Жанр
        /// </summary>
        public Genre Genre { get; set; }

        /// <summary>
        /// Превью
        /// </summary>
        public byte[]? ImagePreview { get; set; }

        public ICollection<Session> Sessions { get; set; }
    }
}
