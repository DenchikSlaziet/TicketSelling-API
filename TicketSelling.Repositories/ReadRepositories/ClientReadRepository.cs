using Microsoft.EntityFrameworkCore;
using TicketSelling.Context.Contracts.Interfaces;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Anchors;
using TicketSelling.Repositories.Contracts.ReadInterfaces;

namespace TicketSelling.Repositories.ReadRepositories
{
    /// <summary>
    /// Реализация <see cref="IClientReadRepository"/>
    /// </summary>
    public class ClientReadRepository : IClientReadRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private IReader reader;

        public ClientReadRepository(IReader reader)
        {
            this.reader = reader;
        }

        Task<List<Client>> IClientReadRepository.GetAllAsync(CancellationToken cancellationToken) 
            => reader.Read<Client>().ToListAsync();

        Task<Client?> IClientReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken) 
            => reader.Read<Client>().FirstOrDefaultAsync(x => x.Id == id);

        Task<Dictionary<Guid, Client>> IClientReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) 
            => reader.Read<Client>().Where(x => ids.Contains(x.Id)).ToDictionaryAsync(x => x.Id);
    }
}
