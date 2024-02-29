namespace TicketSelling.Services.Contracts.Models
{
    /// <summary>
    /// Модель билета
    /// </summary>
    public class TicketModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Киносеанс
        /// </summary>
        public SessionModel Session { get; set; }

        /// <summary>
        /// Клиент
        /// </summary>
        public UserModel User { get; set; }

        /// <summary>
        /// Завхоз
        /// </summary>
        public StaffModel? Staff { get; set; }

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
