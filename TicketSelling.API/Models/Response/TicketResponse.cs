using TicketSelling.API.Enums;

namespace TicketSelling.API.Models.Response
{
    public class TicketResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
 
        /// <summary>
        /// Сущность клиента
        /// </summary>
        public UserResponse? User { get; set; }

        /// <summary>
        /// Сущность фильма
        /// </summary>
        public SessionResponse? Session { get; set; }

        /// <summary>
        /// Сущность персонала
        /// </summary>
        public StaffResponse? Staff { get; set; }

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

        /// <summary>
        /// Способ оплаты
        /// </summary>
        public PaymentMethodResponse PaymentMethod { get; set; }
    }
}
