using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ReadServices;

namespace TicketSelling.Services.ReadServices
{
    public class HallService : IHallService
    {
        private readonly IHallReadRepository hallReadRepository;

        public HallService(IHallReadRepository hallReadRepository)
        {
            this.hallReadRepository = hallReadRepository;
        }

        async Task<IEnumerable<HallModel>> IHallService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await hallReadRepository.GetAllAsync(cancellationToken);

            return result.Select(x => new HallModel
            {
                Id = x.Id,
                Number = x.Number,
                NumberOfSeats = x.NumberOfSeats
            });
        }

        async Task<HallModel?> IHallService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await hallReadRepository.GetByIdAsync(id, cancellationToken);

            if(item == null)
            {
                return null;
            }

            return new HallModel
            {
                Id = item.Id,
                Number = item.Number,
                NumberOfSeats = item.NumberOfSeats
            };
        }
    }
}
