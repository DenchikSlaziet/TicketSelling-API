namespace TicketSelling.Services.Contracts.Exceptions
{
    public class TicketSellingEntityNotFoundException<TEntity> : TicketSellingNotFoundException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TicketSellingEntityNotFoundException{TEntity}"/>
        /// </summary>
        public TicketSellingEntityNotFoundException(Guid id)
            : base($"Сущность {typeof(TEntity)} c id = {id} не найдена.")
        {
        }
    }
}
