namespace TicketSelling.API.Models
{
    /// <summary>
    /// Абстракция
    /// </summary>
    public abstract class PersonResponse
    {
        /// <summary>
        /// ФИО
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}

