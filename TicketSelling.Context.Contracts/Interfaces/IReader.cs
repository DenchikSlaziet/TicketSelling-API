namespace TicketSelling.Context.Contracts.Interfaces
{
    /// <summary>
    /// Интерфейс чтения БД
    /// </summary>
    public interface IReader
    {
        /// <summary>
        /// Возвращает лист сущностей
        /// </summary>
        IQueryable<TEntity> Read<TEntity>() where TEntity : class, IEntity;
    }
}
