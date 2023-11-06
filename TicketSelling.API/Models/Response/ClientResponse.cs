using TicketSelling.Services.Contracts.Models;

namespace TicketSelling.API.Models.Response
{
    /// <summary>
    /// Модель ответа сущности клиента
    /// </summary>
    public class ClientResponse : PersonResponse
    {
        public short Age { get; set; }
    }
}
