﻿using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Repositories.Contracts.ReadInterfaces
{
    /// <summary>
    /// Репозиторий чтения <see cref="Ticket"/>
    /// </summary>
    public interface ITicketReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Ticket"/>
        /// </summary>
        Task<List<Ticket>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Ticket"/> по идентификатору
        /// </summary>
        Task<Ticket?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
