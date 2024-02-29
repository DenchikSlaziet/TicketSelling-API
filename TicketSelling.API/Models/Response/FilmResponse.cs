using TicketSelling.API.Enums;
using TicketSelling.Context.Contracts.Enums;

namespace TicketSelling.API.Models.Response
{
    /// <summary>
    /// Модель ответа сущности фильма
    /// </summary>
    public class FilmResponse
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
        /// Ограничение по возросту
        /// </summary>
        public int Limitation { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }

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
