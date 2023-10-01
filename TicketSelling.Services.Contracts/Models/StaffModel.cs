using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Services.Contracts.Enums;

namespace TicketSelling.Services.Contracts.Models
{
    /// <summary>
    /// Модель персонала
    /// </summary>
    public class StaffModel : PersonModel
    {     
        public Post Post { get; set; } 
    }
}
