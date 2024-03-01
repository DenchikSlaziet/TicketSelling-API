namespace TicketSelling.Services.Contracts.Exceptions
{
    public class TicketSellingNotFoundException : TicketSellingException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TicketSellingNotFoundException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        public TicketSellingNotFoundException(string message)
            : base(message)
        { }
    }
}
