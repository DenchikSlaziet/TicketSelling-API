using Microsoft.EntityFrameworkCore;
using TicketSelling.Context.Contracts.Interfaces;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Anchors;
using TicketSelling.Repositories.Contracts.ReadInterfaces;

namespace TicketSelling.Repositories.ReadRepositories
{
    /// <summary>
    /// Реализация <see cref="ICinemaReadRepository"/>
    /// </summary>
    public class CinemaReadRepository : ICinemaReadRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Reader для связи с бд
        /// </summary>
        private IReader reader;

        public CinemaReadRepository(IReader reader)
        {
            this.reader = reader;
        }

        Task<List<Cinema>> ICinemaReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Cinema>().ToListAsync();

        Task<Cinema?> ICinemaReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Cinema>().FirstOrDefaultAsync(x => x.Id == id);

        Task<Dictionary<Guid, Cinema>> ICinemaReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Cinema>().Where(x => ids.Contains(x.Id)).ToDictionaryAsync(x => x.Id);
    }
}
