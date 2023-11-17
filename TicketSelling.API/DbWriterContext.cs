using TicketSelling.Common.Entity;
using TicketSelling.Common.Entity.InterfaceDB;

namespace TicketSelling.API
{
    public class DbWriterContext : IDbWriterContext
    {
        /// <inheritdoc/>
        public IDbWriter Writer { get; }

        /// <inheritdoc/>
        public IUnitOfWork UnitOfWork { get; }

        /// <inheritdoc/>
        public IDateTimeProvider DateTimeProvider { get; }

        /// <inheritdoc/>
        public string UserName { get; } = "TicketSelling.Api";

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DbWriterContext"/>
        /// </summary>
        /// <remarks>В реальном проекте надо добавлять IIdentity для доступа к
        /// информации об авторизации</remarks>
        public DbWriterContext(
            IDbWriter writer,
            IUnitOfWork unitOfWork, IDateTimeProvider provider)
        {
            Writer = writer;
            UnitOfWork = unitOfWork;
            DateTimeProvider = provider;
        }
    }
}
