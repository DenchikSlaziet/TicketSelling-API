using Microsoft.EntityFrameworkCore;
using TicketSelling.Context.Contracts.Interfaces;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Anchors;
using TicketSelling.Repositories.Contracts.ReadInterfaces;

namespace TicketSelling.Repositories.ReadRepositories
{
    /// <summary>
    /// Реализация <see cref="ITicketReadRepository"/>
    /// </summary>
    public class TicketReadRepositiry : ITicketReadRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private IReader reader;

        public TicketReadRepositiry(IReader reader)
        {
            this.reader = reader;
        }

        Task<List<Ticket>> ITicketReadRepository.GetAllAsync(CancellationToken cancellationToken) 
            => reader.Read<Ticket>().ToListAsync();

        Task<Ticket?> ITicketReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken) 
            => reader.Read<Ticket>().FirstOrDefaultAsync(x => x.Id == id);
    }
}
