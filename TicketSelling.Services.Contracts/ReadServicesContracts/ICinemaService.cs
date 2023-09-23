using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Services.Contracts.Models;

namespace TicketSelling.Services.Contracts.ReadServices
{
    public interface ICinemaService
    {
        /// <summary>
        /// Получить список всех <see cref="CinemaModel"/>
        /// </summary>
        Task<IEnumerable<CinemaModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="CinemaModel"/> по идентификатору
        /// </summary>
        Task<CinemaModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
