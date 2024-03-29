﻿using Microsoft.EntityFrameworkCore;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Common.Entity.Repositories;
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
        private IDbRead reader;

        public StaffReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }
        Task<IReadOnlyCollection<Staff>> IStaffReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Staff>()
                .NotDeletedAt()
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ThenBy(x => x.Patronymic)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Staff?> IStaffReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken) 
            => reader.Read<Staff>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Staff>> IStaffReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) 
            => reader.Read<Staff>()
                .ByIds(ids)
                .NotDeletedAt()
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ThenBy(x => x.Patronymic)
                .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<Staff?> IStaffReadRepository.GetNotDeletedByIdAsync(Guid id, CancellationToken cancellationToken)
        => reader.Read<Staff>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<bool> IStaffReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Staff>()
            .NotDeletedAt()
            .AnyAsync(x => x.Id == id, cancellationToken);
    }
}
