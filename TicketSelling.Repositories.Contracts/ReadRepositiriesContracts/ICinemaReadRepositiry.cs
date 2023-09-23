using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Repositories.Contracts.ReadInterfaces
{
    /// <summary>
    /// Репозиторий чтения <see cref="Cinema"/>
    /// </summary>
    public interface ICinemaReadRepositiry
    {
        /// <summary>
        /// Получить список всех <see cref="Cinema"/>
        /// </summary>
        Task<List<Cinema>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Cinema"/> по идентификатору
        /// </summary>
        Task<Cinema?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
