namespace TicketSelling.Context.Contracts.Models
{
    /// <summary>
    /// Зал
    /// </summary>
    public class Hall : BaseAuditEntity
    {
        /// <summary>
        /// Номер зала
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Кол-во мест в ряду
        /// </summary>
        public int CountPlaceInRow { get; set; }

        /// <summary>
        /// Кол-во рядов
        /// </summary>
        public int CountRow { get; set; }

        public ICollection<Session> Sessions { get; set; }
    }
}
