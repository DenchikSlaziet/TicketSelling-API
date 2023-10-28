using Microsoft.EntityFrameworkCore;
using TicketSelling.Context.Contracts.Interfaces;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Anchors;
using TicketSelling.Repositories.Contracts.ReadInterfaces;

namespace TicketSelling.Repositories.ReadRepositories
{
    /// <summary>
    /// Реализация <see cref="IHallReadRepository"/>
    /// </summary>
    public class HallReadRepository : IHallReadRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private IReader reader;

        public HallReadRepository(IReader reader)
        {
            this.reader = reader;
        }

        Task<List<Hall>> IHallReadRepository.GetAllAsync(CancellationToken cancellationToken) 
            => reader.Read<Hall>().ToListAsync();

        Task<Hall?> IHallReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken) 
            => reader.Read<Hall>().FirstOrDefaultAsync(x => x.Id == id);

        Task<Dictionary<Guid ,Hall>> IHallReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) 
            => reader.Read<Hall>().Where(x => ids.Contains(x.Id)).ToDictionaryAsync(x => x.Id);
    }
}
