using TicketSelling.Context.Contracts;
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
        private ITicketSellingContext context;

        public HallReadRepository(ITicketSellingContext context)
        {
            this.context = context;
        }

        Task<List<Hall>> IHallReadRepository.GetAllAsync(CancellationToken cancellationToken) 
            => Task.FromResult(context.Halls.ToList());

        Task<Hall?> IHallReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken) 
            => Task.FromResult(context.Halls.FirstOrDefault(x => x.Id == id));

        Task<Dictionary<Guid ,Hall>> IHallReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) 
            => Task.FromResult(context.Halls.Where(x => ids.Contains(x.Id)).ToDictionary(x => x.Id));
    }
}
