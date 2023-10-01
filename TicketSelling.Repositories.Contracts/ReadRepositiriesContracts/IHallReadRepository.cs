using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Repositories.Contracts.ReadInterfaces
{
    /// <summary>
    /// Репозиторий чтения <see cref="Hall"/>
    /// </summary>
    public interface IHallReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Hall"/>
        /// </summary>
        Task<List<Hall>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Hall"/> по идентификатору
        /// </summary>
        Task<Hall?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Hall"/> по идентификаторам
        /// </summary>
        Task<List<Hall>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    }
}
