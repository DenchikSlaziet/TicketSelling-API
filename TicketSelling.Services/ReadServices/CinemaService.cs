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
    public class CinemaService : ICinemaService
    {
        private readonly ICinemaReadRepositiry cinemaReadRepositiry;

        public CinemaService(ICinemaReadRepositiry cinemaReadRepositiry)
        {
            this.cinemaReadRepositiry = cinemaReadRepositiry;
        }

        async Task<IEnumerable<CinemaModel>> ICinemaService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await cinemaReadRepositiry.GetAllAsync(cancellationToken);

            return result.Select(x => new CinemaModel
            {
                Id = x.Id,
                Address = x.Address,
                Title = x.Title
            });
        }

        async Task<CinemaModel?> ICinemaService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
           var item = await cinemaReadRepositiry.GetByIdAsync(id, cancellationToken);

           if(item == null) 
           { 
                return null;
           }

           return new CinemaModel
           {
               Id = item.Id,
               Address = item.Address, 
               Title = item.Title
           };
        }
    }
}
