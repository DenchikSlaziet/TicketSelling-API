using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Repositories.Contracts.ReadInterfaces
{
    /// <summary>
    /// Репозиторий чтения <see cref="Film"/>
    /// </summary>
    public interface IFilmReadRepository
    {       
        /// <summary>
        /// Получить список всех <see cref="Film"/>
        /// </summary>
        Task<List<Film>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Film"/> по идентификатору
        /// </summary>
        Task<Film?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Film"/> по идентификаторам
        /// </summary>
        Task<List<Film>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    }
}
