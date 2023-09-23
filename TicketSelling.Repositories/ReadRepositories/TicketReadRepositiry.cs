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
    /// Реализация <see cref="ITicketReadRepositiry"/>
    /// </summary>
    public class TicketReadRepositiry : ITicketReadRepositiry
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private ITicketSellingContext context;

        public TicketReadRepositiry(ITicketSellingContext context)
        {
            this.context = context;
        }

        Task<List<Ticket>> ITicketReadRepositiry.GetAllAsync(CancellationToken cancellationToken) =>
            Task.FromResult(context.Tickets.ToList());


        Task<Ticket?> ITicketReadRepositiry.GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
            Task.FromResult(context.Tickets.FirstOrDefault(x => x.Id == id));
    }
}
