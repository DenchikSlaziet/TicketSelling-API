using TicketSelling.Services.Contracts.Enums;

namespace TicketSelling.Services.Contracts.Models
{
    /// <summary>
    /// Модель персонала
    /// </summary>
    public class StaffModel : PersonModel
    {     
        public PostModel Post { get; set; } 
    }
}
