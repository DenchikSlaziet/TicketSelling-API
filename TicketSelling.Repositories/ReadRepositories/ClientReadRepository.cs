using Microsoft.EntityFrameworkCore;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Common.Entity.Repositories;
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
        private IDbRead reader;

        public ClientReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Client>> IClientReadRepository.GetAllAsync(CancellationToken cancellationToken) 
            => reader.Read<Client>().OrderBy(x => x.FirstName).ToReadOnlyCollectionAsync(cancellationToken);

        Task<Client?> IClientReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken) 
            => reader.Read<Client>().ById(id).FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Client>> IClientReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) 
            => reader.Read<Client>().ByIds(ids)
                .OrderBy(x => x.FirstName).ToDictionaryAsync(x => x.Id, cancellationToken);
    }
}
