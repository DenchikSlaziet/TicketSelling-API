using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSelling.Context.Contracts.Models
{
    /// <summary>
    /// Билет
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор Зала
        /// </summary>
        public Guid HallId { get; set; }

        /// <summary>
        /// Идентификатор Кинотеатра
        /// </summary>
        public Guid CinemaId { get; set; }

        /// <summary>
        /// Идентификатор Фильма
        /// </summary>
        public Guid FilmId { get; set; }

        /// <summary>
        /// Идентификатор Клиента
        /// </summary>
        public Guid ClientId { get; set; }

        /// <summary>
        /// Идентификатор завхоза
        /// </summary>
        public Guid StaffId { get; set; }

        /// <summary>
        /// Ряд
        /// </summary>
        public short Row { get; set; }

        /// <summary>
        /// Место
        /// </summary>
        public short Place { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Дата и врремя проведения фильма
        /// </summary>
        public DateTimeOffset Date { get; set; }
    }
}
