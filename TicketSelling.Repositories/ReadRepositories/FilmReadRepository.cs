﻿using Microsoft.EntityFrameworkCore;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Common.Entity.Repositories;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Anchors;
using TicketSelling.Repositories.Contracts.ReadInterfaces;

namespace TicketSelling.Repositories.ReadRepositories
{
    /// <summary>
    /// Реализация <see cref="IFilmReadRepository"/>
    /// </summary>
    public class FilmReadRepository : IFilmReadRepository, IRepositoryAnchor
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private IDbRead reader;

        public FilmReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Film>> IFilmReadRepository.GetAllAsync(CancellationToken cancellationToken) 
            => reader.Read<Film>()
                .NotDeletedAt()
                .OrderBy(x => x.Title)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Film?> IFilmReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken) 
            => reader.Read<Film>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Film>> IFilmReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) 
            => reader.Read<Film>()
                .ByIds(ids)
                .OrderBy(x => x.Title)
                .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<Film?> IFilmReadRepository.GetNotDeletedByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Film>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Film>> IFilmReadRepository.GetNotDeletedByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
            => reader.Read<Film>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.Title)
                .ToDictionaryAsync(x => x.Id, cancellationToken);

        Task<bool> IFilmReadRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Film>()
            .NotDeletedAt()
            .AnyAsync(x => x.Id == id, cancellationToken);
    }
}
