﻿using Microsoft.EntityFrameworkCore;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Common.Entity.Repositories;
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
        private IDbRead reader;

        public TicketReadRepositiry(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Ticket>> ITicketReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Ticket>().OrderBy(x => x.Date).ToReadOnlyCollectionAsync(cancellationToken);

        Task<Ticket?> ITicketReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken) 
            => reader.Read<Ticket>().ById(id).FirstOrDefaultAsync(cancellationToken);
    }
}
