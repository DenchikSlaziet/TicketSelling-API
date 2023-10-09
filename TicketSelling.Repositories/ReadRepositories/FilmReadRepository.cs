using TicketSelling.Context.Contracts;
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
        private ITicketSellingContext context;

        public FilmReadRepository(ITicketSellingContext context)
        {
            this.context = context;
        }

        Task<List<Film>> IFilmReadRepository.GetAllAsync(CancellationToken cancellationToken) 
            => Task.FromResult(context.Films.ToList());

        Task<Film?> IFilmReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken) 
            => Task.FromResult(context.Films.FirstOrDefault(x => x.Id == id));

        Task<Dictionary<Guid, Film>> IFilmReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) 
            => Task.FromResult(context.Films.Where(x => ids.Contains(x.Id)).ToDictionary(x => x.Id));
    }
}
