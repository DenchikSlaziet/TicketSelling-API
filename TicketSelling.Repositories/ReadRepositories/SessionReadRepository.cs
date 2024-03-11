using Microsoft.EntityFrameworkCore;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Common.Entity.Repositories;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Anchors;
using TicketSelling.Repositories.Contracts.ReadRepositiriesContracts;

namespace TicketSelling.Repositories.ReadRepositories
{
    public class SessionReadRepository : ISessionReadRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private IDbRead reader;

        public SessionReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Session>> ISessionReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Session>()
                .NotDeletedAt()
                .OrderBy(x => x.StartDateTime)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Session?> ISessionReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Session>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Session>> ISessionReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Session>()
                .ByIds(ids)
                .NotDeletedAt()
                .OrderBy(x => x.StartDateTime)
                .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<Session?> ISessionReadRepository.GetNotDeletedByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Session>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<bool> ISessionReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Session>()
            .NotDeletedAt()
            .AnyAsync(x => x.Id == id, cancellationToken);
    }
}
