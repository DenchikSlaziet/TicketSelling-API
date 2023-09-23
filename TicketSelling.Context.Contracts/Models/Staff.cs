using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Context.Contracts.Enums;

namespace TicketSelling.Context.Contracts.Models
{
    /// <summary>
    /// Кассир
    /// </summary>
    public class Staff : Person
    {
        /// <summary>
        /// Должность
        /// </summary>
        public Post Post { get; set; }
    }
}
