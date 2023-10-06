namespace TicketSelling.API.Models
{
    /// <summary>
    /// Абстракция
    /// </summary>
    public abstract class PersonResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}

