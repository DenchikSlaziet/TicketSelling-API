namespace TicketSelling.Context.Contracts.Models
{
    /// <summary>
    /// Киносеанс
    /// </summary>
    public class Session : BaseAuditEntity
    {
        /// <summary>
        /// Идентификатор фильма
        /// </summary>
        public Guid FilmId { get; set; }
        public Film Film { get; set; }

        /// <summary>
        /// Идентификатор зала
        /// </summary>
        public Guid HallId { get; set; }
        public Hall Hall { get; set; }

        /// <summary>
        /// Дата и время начала
        /// </summary>
        public DateTimeOffset StartDateTime { get; set; }

        /// <summary>
        /// Дата и время конца
        /// </summary>
        public DateTimeOffset EndDateTime { get; set;}

        public ICollection<Ticket> Tickets { get; set; }
    }
}
