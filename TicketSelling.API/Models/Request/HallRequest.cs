using TicketSelling.API.Models.CreateRequest;

namespace TicketSelling.API.Models.Request
{
    public class HallRequest : CreateHallRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
