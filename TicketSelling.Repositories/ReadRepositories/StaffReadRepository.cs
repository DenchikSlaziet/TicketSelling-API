using Microsoft.EntityFrameworkCore;
using TicketSelling.Context.Contracts.Interfaces;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Anchors;
using TicketSelling.Repositories.Contracts.ReadInterfaces;

namespace TicketSelling.Repositories.ReadRepositories
{
    /// <summary>
    /// Реализация <see cref="IStaffReadRepository"/>
    /// </summary>
    public class StaffReadRepository : IStaffReadRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private IReader reader;

        public StaffReadRepository(IReader reader)
        {
            this.reader = reader;
        }
        Task<List<Staff>> IStaffReadRepository.GetAllAsync(CancellationToken cancellationToken) 
            => reader.Read<Staff>().ToListAsync();

        Task<Staff?> IStaffReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken) 
            => reader.Read<Staff>().FirstOrDefaultAsync(x => x.Id == id);

        Task<Dictionary<Guid, Staff>> IStaffReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) 
            => reader.Read<Staff>().Where(x => ids.Contains(x.Id)).ToDictionaryAsync(x => x.Id);
    }
}
