namespace TicketSelling.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания билета
    /// </summary>
    public class CreateTicketRequest
    {
        /// <summary>
        /// Идентификатор Клиента
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Идентификатор Сеанса
        /// </summary>
        public Guid SessionId { get; set; }

        /// <summary>
        /// Идентификатор Завхоза
        /// </summary>
        public Guid? StaffId { get; set; }

        /// <summary>
        /// Ряд
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Место
        /// </summary>
        public int Place { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Дата и время покупки билета
        /// </summary>
        public DateTimeOffset DatePayment { get; set; }
    }
}
