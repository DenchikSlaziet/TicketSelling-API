﻿using Microsoft.EntityFrameworkCore;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Common.Entity.Repositories;
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
        private IDbRead reader;

        public HallReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Hall>> IHallReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Hall>()
                .NotDeletedAt()
                .OrderBy(x => x.Number)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Hall?> IHallReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Hall>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Hall>> IHallReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Hall>()
                .ByIds(ids)
                .OrderBy(x => x.Number).ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<Hall?> IHallReadRepository.GetNotDeletedByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Hall>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Hall>> IHallReadRepository.GetNotDeletedByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Hall>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.Number).ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<bool> IHallReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Hall>().NotDeletedAt()
            .AnyAsync(x => x.Id == id, cancellationToken);
    }
}