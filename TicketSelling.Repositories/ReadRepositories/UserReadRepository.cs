using Microsoft.EntityFrameworkCore;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Common.Entity.Repositories;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Anchors;
using TicketSelling.Repositories.Contracts.ReadInterfaces;

namespace TicketSelling.Repositories.ReadRepositories
{
    /// <summary>
    /// Реализация <see cref="IUserReadRepository"/>
    /// </summary>
    public class UserReadRepository : IUserReadRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private IDbRead reader;

        public UserReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<User>> IUserReadRepository.GetAllAsync(CancellationToken cancellationToken) 
            => reader.Read<User>()
                .NotDeletedAt()
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ThenBy(x => x.Patronymic)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<User?> IUserReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken) 
            => reader.Read<User>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, User>> IUserReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) 
            => reader.Read<User>()
                .ByIds(ids)
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ThenBy(x => x.Patronymic)
                .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<User?> IUserReadRepository.GetNotDeletedByIdAsync(Guid id, CancellationToken cancellationToken)
             => reader.Read<User>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<bool> IUserReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<User>()
            .NotDeletedAt()
            .AnyAsync(x => x.Id == id, cancellationToken);
    }
}
