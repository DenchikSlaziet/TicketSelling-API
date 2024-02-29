namespace TicketSelling.Services.Contracts.Exceptions
{
    public class TicketSellingInvalidOperationException : TicketSellingException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TicketSellingInvalidOperationException"/>
        /// с указанием сообщения об ошибке
        /// </summary>
        public TicketSellingInvalidOperationException(string message)
            : base(message)
        {

        }
    }
}
