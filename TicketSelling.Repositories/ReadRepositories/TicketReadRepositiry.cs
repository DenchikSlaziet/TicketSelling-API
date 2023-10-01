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
    /// Реализация <see cref="ITicketReadRepository"/>
    /// </summary>
    public class TicketReadRepositiry : ITicketReadRepository
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private ITicketSellingContext context;

        public TicketReadRepositiry(ITicketSellingContext context)
        {
            this.context = context;
        }

        Task<List<Ticket>> ITicketReadRepository.GetAllAsync(CancellationToken cancellationToken) =>
            Task.FromResult(context.Tickets.ToList());


        Task<Ticket?> ITicketReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
            Task.FromResult(context.Tickets.FirstOrDefault(x => x.Id == id));
    }
}
