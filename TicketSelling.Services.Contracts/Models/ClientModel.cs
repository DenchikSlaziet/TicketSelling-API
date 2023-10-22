namespace TicketSelling.Services.Contracts.Models
{
    public class ClientModel : PersonModel
    {
        public string Email { get; set; } = string.Empty;
        public short Age { get; set; }
    }
}
