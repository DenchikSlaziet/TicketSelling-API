namespace TicketSelling.Services.Contracts.Models
{
    public class SessionModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Фильм
        /// </summary>
        public FilmModel Film { get; set; }

        /// <summary>
        /// Зал
        /// </summary>
        public HallModel Hall { get; set; }

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
