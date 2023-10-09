using TicketSelling.API.Enums;

namespace TicketSelling.API.Models
{
    /// <summary>
    /// Модель ответа сущности персонала
    /// </summary>
    public class StaffResponse : PersonResponse
    {
        /// <summary>
        /// Должность
        /// </summary>
        public PostResponse Post { get; set; }
    }
}
