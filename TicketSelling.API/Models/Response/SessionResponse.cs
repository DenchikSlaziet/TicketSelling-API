namespace TicketSelling.API.Models.Response
{
    /// <summary>
    /// Модель ответа киносеанса
    /// </summary>
    public class SessionResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Фильм
        /// </summary>
        public FilmResponse Film { get; set; }

        /// <summary>
        /// Зал
        /// </summary>
        public HallResponse Hall { get; set; }

        /// <summary>
        /// Дата и время начала
        /// </summary>
        public DateTimeOffset StartDateTime { get; set; }

        /// <summary>
        /// Дата и время конца
        /// </summary>
        public DateTimeOffset EndDateTime { get; set; }

    }
}
