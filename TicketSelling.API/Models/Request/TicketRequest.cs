using TicketSelling.API.Models.CreateRequest;

namespace TicketSelling.API.Models.Request
{
    public class TicketRequest : CreateTicketRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
