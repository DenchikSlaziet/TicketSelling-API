using TicketSelling.API.Enums;

namespace TicketSelling.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания фильма
    /// </summary>
    public class CreateFilmRequest
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
        public GenreResponse Genre { get; set; }

        /// <summary>
        /// Превью
        /// </summary>
        public byte[]? ImagePreview { get; set; }
    }
}
