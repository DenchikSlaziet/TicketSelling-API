namespace TicketSelling.Context.Contracts.Interfaces
{
    /// <summary>
    /// Интерфейс сохранения БД
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Сохраняет все изменения контекста
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
