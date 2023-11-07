using TicketSelling.API.Models.CreateRequest;

namespace TicketSelling.API.Models.Request
{
    public class CinemaRequest : CreateCinemaRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
