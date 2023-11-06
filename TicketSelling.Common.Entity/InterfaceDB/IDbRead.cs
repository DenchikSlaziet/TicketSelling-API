using TicketSelling.Common.Entity.EntityInterface;

namespace TicketSelling.Common.Entity.InterfaceDB
{
    public interface IDbRead
    {
        /// <summary>
        /// Предоставляет функциональные возможности для выполнения запросов
        /// </summary> 
        IQueryable<TEntity> Read<TEntity>() where TEntity : class, IEntity;
    }
}
