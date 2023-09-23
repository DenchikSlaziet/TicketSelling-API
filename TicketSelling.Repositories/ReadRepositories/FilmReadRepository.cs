using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Context.Contracts;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Contracts.ReadInterfaces;

namespace TicketSelling.Repositories.ReadRepositories
{
    /// <summary>
    /// Реализация <see cref="IFilmReadRepository"/>
    /// </summary>
    public class FilmReadRepository : IFilmReadRepository
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private ITicketSellingContext context;

        public FilmReadRepository(ITicketSellingContext context)
        {
            this.context = context;
        }

        Task<List<Film>> IFilmReadRepository.GetAllAsync(CancellationToken cancellationToken) =>
            Task.FromResult(context.Films.ToList());


        Task<Film?> IFilmReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
            Task.FromResult(context.Films.FirstOrDefault(x => x.Id == id));
    }
}
