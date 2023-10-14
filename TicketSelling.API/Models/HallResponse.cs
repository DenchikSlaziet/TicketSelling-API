namespace TicketSelling.API.Models
{
    /// <summary>
    /// Модель ответа сущности зала
    /// </summary>
    public class HallResponse
    {
        /// <summary>
        /// Номер зала
        /// </summary>
        public short Number { get; set; }

        /// <summary>
        /// Кол-во мест
        /// </summary>
        public short NumberOfSeats { get; set; }
    }
}
