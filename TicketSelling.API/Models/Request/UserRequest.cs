namespace TicketSelling.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса изменения клиента
    /// </summary>
    public class UserRequest : CreateUserRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
