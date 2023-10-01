using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Context;
using TicketSelling.Context.Contracts;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Contracts.ReadInterfaces;

namespace TicketSelling.Repositories.ReadRepositories
{
    /// <summary>
    /// Реализация <see cref="ICinemaReadRepository"/>
    /// </summary>
    public class CinemaReadRepository : ICinemaReadRepository
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private ITicketSellingContext context;

        public CinemaReadRepository(ITicketSellingContext context)
        {
            this.context = context;
        }

        Task<List<Cinema>> ICinemaReadRepository.GetAllAsync(CancellationToken cancellationToken) =>
            Task.FromResult(context.Cinemas.ToList());

        Task<Cinema?> ICinemaReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
            Task.FromResult(context.Cinemas.FirstOrDefault(x =>  x.Id == id));

        Task<List<Cinema>> ICinemaReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) =>
            Task.FromResult(context.Cinemas.Where(x=> ids.Contains(x.Id)).ToList());
     
    }
}
