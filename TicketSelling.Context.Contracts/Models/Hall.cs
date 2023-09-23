using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSelling.Context.Contracts.Models
{
    /// <summary>
    /// Зал
    /// </summary>
    public class Hall
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Номер зала
        /// </summary>
        public short Number { get; set; }

        /// <summary>
        /// Кол-во мест
        /// </summary>
        public short NumberOfSeats { get; set; }
    }
}
