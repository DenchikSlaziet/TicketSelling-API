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
    /// Реализация <see cref="IHallReadRepository"/>
    /// </summary>
    public class HallReadRepository : IHallReadRepository
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private ITicketSellingContext context;

        public HallReadRepository(ITicketSellingContext context)
        {
            this.context = context;
        }

        Task<List<Hall>> IHallReadRepository.GetAllAsync(CancellationToken cancellationToken) =>
           Task.FromResult(context.Halls.ToList());

        Task<Hall?> IHallReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
            Task.FromResult(context.Halls.FirstOrDefault(x => x.Id == id));
    }
}
