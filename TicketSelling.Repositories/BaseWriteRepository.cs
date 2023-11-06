using System.Diagnostics.CodeAnalysis;
using TicketSelling.Common.Entity.EntityInterface;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Repositories.Contracts.WriteRepositoriesContracts;

namespace TicketSelling.Repositories
{
    public abstract class BaseWriteRepository<T> : IRepositoryWriter<T> where T : class, IEntity
    {
        /// <inheritdoc cref="IDbWriterContext"/>
        private readonly IDbWriterContext writerContext;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="BaseWriteRepository{T}"/>
        /// </summary>
        protected BaseWriteRepository(IDbWriterContext writerContext)
        {
            this.writerContext = writerContext;
        }

        /// <inheritdoc cref="IRepositoryWriter{T}"/>
        public virtual void Add([NotNull] T entity)
        {
            if (entity is IEntityWithId entityWithId &&
                entityWithId.Id == Guid.Empty)
            {
                entityWithId.Id = Guid.NewGuid();
            }
            writerContext.Writer.Add(entity);
        }

        /// <inheritdoc cref="IRepositoryWriter{T}"/>
        public virtual void Update([NotNull] T entity)
        {
            writerContext.Writer.Update(entity);
        }

        /// <inheritdoc cref="IRepositoryWriter{T}"/>
        public virtual void Delete([NotNull] T entity)
        {          
            writerContext.Writer.Delete(entity);           
        }    
    }
}
