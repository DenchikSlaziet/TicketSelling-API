using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Repositories.Contracts.ReadRepositiriesContracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="Session"/>
    /// </summary>
    public interface ISessionReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Session"/>
        /// </summary>
        Task<IReadOnlyCollection<Session>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить неудаленный <see cref="Session"/> по идентификатору
        /// </summary>
        Task<Session?> GetNotDeletedByIdAsync(Guid id, CancellationToken cancellationToken);

        // <summary>
        /// Получить любой <see cref="Session"/> по идентификатору
        /// </summary>
        Task<Session?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить любые <see cref="Session"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Session>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="Session"/> в коллеции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
