using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Repositories.Contracts.ReadInterfaces
{
    /// <summary>
    /// Репозиторий чтения <see cref="Staff"/>
    /// </summary>
    public interface IStaffReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Staff"/>
        /// </summary>
        Task<List<Staff>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Staff"/> по идентификатору
        /// </summary>
        Task<Staff?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Staff"/> по идентификаторам
        /// </summary>
        Task<List<Staff>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    }
}
