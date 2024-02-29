using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Repositories.Contracts.ReadInterfaces
{
    /// <summary>
    /// Репозиторий чтения <see cref="User"/>
    /// </summary>
    public interface IUserReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="User"/>
        /// </summary>
        Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="User"/> по идентификатору
        /// </summary>
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить неудаленные <see cref="User"/> по идентификатору
        /// </summary>
        Task<User?> GetNotDeletedByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить любые <see cref="User"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid,User>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="User"/> в коллеции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
