using TicketSelling.API.Models.CreateRequest;

namespace TicketSelling.API.Models.Request
{

    /// <summary>
    /// Модель запроса изменения киносеанса
    /// </summary>
    public class SessionRequest : CreateSessionRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
