using TicketSelling.Services.Contracts.Enums;

namespace TicketSelling.Services.Contracts.Models
{
    /// <summary>
    /// Модель фильма
    /// </summary>
    public class FilmModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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
        public GenreModel Genre { get; set; }

        /// <summary>
        /// Превью
        /// </summary>
        public byte[]? ImagePreview { get; set; }
    }
}
