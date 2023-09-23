namespace TicketSelling.API.Models
{
    /// <summary>
    /// Модель ответа сущности кинотеатра
    /// </summary>
    public class CinemaResponse
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
        /// Адрес
        /// </summary>
        public string Address { get; set; } = string.Empty;
    }
}
