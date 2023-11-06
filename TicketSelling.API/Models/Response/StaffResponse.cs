using TicketSelling.API.Enums;

namespace TicketSelling.API.Models.Response
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
