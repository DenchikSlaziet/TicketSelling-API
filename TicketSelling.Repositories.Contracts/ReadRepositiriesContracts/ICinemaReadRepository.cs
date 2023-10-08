using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Repositories.Contracts.ReadInterfaces
{
    /// <summary>
    /// Репозиторий чтения <see cref="Cinema"/>
    /// </summary>
    public interface ICinemaReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Cinema"/>
        /// </summary>
        Task<List<Cinema>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Cinema"/> по идентификатору
        /// </summary>
        Task<Cinema?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Cinema"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid,Cinema>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    }
}
