namespace TicketSelling.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания киносеанса
    /// </summary>
    public class CreateSessionRequest
    {
        /// <summary>
        /// Идентификатор Фильма
        /// </summary>
        public Guid FilmId { get; set; }

        /// <summary>
        /// Идентификатор Зала
        /// </summary>
        public Guid HallId { get; set; }

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
