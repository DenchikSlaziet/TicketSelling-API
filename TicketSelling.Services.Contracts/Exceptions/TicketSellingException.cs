namespace TicketSelling.Services.Contracts.Exceptions
{
    public abstract class TicketSellingException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TicketSellingException"/> без параметров
        /// </summary>
        protected TicketSellingException() { }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TicketSellingException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        protected TicketSellingException(string message)
            : base(message) { }
    }
}
