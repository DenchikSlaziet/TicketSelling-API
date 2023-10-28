using System.Diagnostics.CodeAnalysis;

namespace TicketSelling.Context.Contracts.Interfaces
{
    /// <summary>
    /// Интерфейс изменения БД
    /// </summary>
    public interface IWriter
    {
        /// <summary>
        /// Добавить новую запись
        /// </summary>
        void Add<TEntity>([NotNull] TEntity entity) where TEntity : class, IEntity;

        /// <summary>
        /// Изменить запись
        /// </summary>
        void Update<TEntity>([NotNull] TEntity entity) where TEntity : class, IEntity;

        /// <summary>
        /// Удалить запись
        /// </summary>
        void Delete<TEntity>([NotNull] TEntity entity) where TEntity : class, IEntity;
    }
}
