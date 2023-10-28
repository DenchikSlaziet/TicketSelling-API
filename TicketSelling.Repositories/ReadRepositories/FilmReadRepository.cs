using Microsoft.EntityFrameworkCore;
using TicketSelling.Context.Contracts.Interfaces;
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
        private IReader reader;

        public FilmReadRepository(IReader reader)
        {
            this.reader = reader;
        }

        Task<List<Film>> IFilmReadRepository.GetAllAsync(CancellationToken cancellationToken) 
            => reader.Read<Film>().ToListAsync();

        Task<Film?> IFilmReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken) 
            => reader.Read<Film>().FirstOrDefaultAsync(x => x.Id == id);

        Task<Dictionary<Guid, Film>> IFilmReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) 
            => reader.Read<Film>().Where(x => ids.Contains(x.Id)).ToDictionaryAsync(x => x.Id);
    }
}
