using TicketSelling.API.Models.CreateRequest;

namespace TicketSelling.API.Models.Request
{
    public class FilmRequest : CreateFilmRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
