using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Repositories.Contracts.ReadInterfaces
{
    /// <summary>
    /// Репозиторий чтения <see cref="Client"/>
    /// </summary>
    public interface IClientReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Client"/>
        /// </summary>
        Task<List<Client>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Client"/> по идентификатору
        /// </summary>
        Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Client"/> по идентификаторам
        /// </summary>
        Task<List<Client>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    }
}
