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
    /// Реализация <see cref="IClientReadRepository"/>
    /// </summary>
    public class ClientReadRepository : IClientReadRepository
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private ITicketSellingContext context;

        public ClientReadRepository(ITicketSellingContext context)
        {
            this.context = context;
        }

        Task<List<Client>> IClientReadRepository.GetAllAsync(CancellationToken cancellationToken) =>
            Task.FromResult(context.Clients.ToList());

        Task<Client?> IClientReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken) => 
            Task.FromResult(context.Clients.FirstOrDefault(x => x.Id == id));
    }
}
