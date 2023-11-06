using TicketSelling.API.Models.CreateRequest;

namespace TicketSelling.API.Models.Request
{
    public class ClientRequest : CreateClientRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
