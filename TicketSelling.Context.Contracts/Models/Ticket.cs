﻿using TicketSelling.Context.Contracts.Enums;

namespace TicketSelling.Context.Contracts.Models
{
    /// <summary>
    /// Билет
    /// </summary>
    public class Ticket : BaseAuditEntity
    {
        /// <summary>
        /// Идентификатор киносеанса
        /// </summary>
        public Guid SessionId { get; set; }
        public Session Session { get; set; }

        /// <summary>
        /// Идентификатор Клиента
        /// </summary>
        public Guid UserId { get; set; }
        public User User { get; set; }

        /// <summary>
        /// Идентификатор завхоза
        /// </summary>
        public Guid? StaffId { get; set; }
        public Staff? Staff { get; set; }

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
        public PaymentMethod PaymentMethod { get; set; }
    }
}
