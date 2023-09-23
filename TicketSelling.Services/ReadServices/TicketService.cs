using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ReadServices;

namespace TicketSelling.Services.ReadServices
{
    public class TicketService : ITicketService
    {
        private readonly ITicketReadRepositiry ticketReadRepositiry;

        public TicketService(ITicketReadRepositiry ticketReadRepositiry)
        {
            this.ticketReadRepositiry = ticketReadRepositiry;
        }

        async Task<IEnumerable<TicketModel>> ITicketService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await ticketReadRepositiry.GetAllAsync(cancellationToken);

            return result.Select(x => new TicketModel
            {
                Id = x.Id,
                FilmId = x.FilmId,
                CinemaId = x.CinemaId,
                ClientId = x.ClientId,
                Date = x.Date,
                HallId = x.HallId,
                Place = x.Place,
                Price = x.Price,
                Row = x.Row
            });
        }

        async Task<TicketModel?> ITicketService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await ticketReadRepositiry.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return null;
            }

            return new TicketModel
            {
                Id = item.Id,
                FilmId = item.FilmId,
                CinemaId = item.CinemaId,
                ClientId = item.ClientId,
                Date = item.Date,
                HallId = item.HallId,
                Place = item.Place,
                Price = item.Price,
                Row = item.Row
            };
        }
    }
}
