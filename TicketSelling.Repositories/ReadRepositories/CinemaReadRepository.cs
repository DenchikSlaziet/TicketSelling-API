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
    /// Реализация <see cref="ICinemaReadRepositiry"/>
    /// </summary>
    public class CinemaReadRepository : ICinemaReadRepositiry
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private ITicketSellingContext context;

        public CinemaReadRepository(ITicketSellingContext context)
        {
            this.context = context;
        }

        Task<List<Cinema>> ICinemaReadRepositiry.GetAllAsync(CancellationToken cancellationToken) =>
            Task.FromResult(context.Cinemas.ToList());

        Task<Cinema?> ICinemaReadRepositiry.GetByIdAsync(Guid id, CancellationToken cancellationToken)=>
            Task.FromResult(context.Cinemas.FirstOrDefault(x =>  x.Id == id));
    }
}
