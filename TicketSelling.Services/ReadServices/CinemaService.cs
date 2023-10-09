using AutoMapper;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Services.Anchors;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ReadServices;

namespace TicketSelling.Services.ReadServices
{
    public class CinemaService : ICinemaService, IServiceAnchor
    {
        private readonly ICinemaReadRepository cinemaReadRepositiry;
        private readonly IMapper mapper;

        public CinemaService(ICinemaReadRepository cinemaReadRepositiry, IMapper mapper)
        {
            this.cinemaReadRepositiry = cinemaReadRepositiry;
            this.mapper = mapper;
        }

        async Task<IEnumerable<CinemaModel>> ICinemaService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await cinemaReadRepositiry.GetAllAsync(cancellationToken);
            return result.Select(x => mapper.Map<CinemaModel>(x));
        }

        async Task<CinemaModel?> ICinemaService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
           var item = await cinemaReadRepositiry.GetByIdAsync(id, cancellationToken);

           if(item == null) 
           { 
                return null;
           }
           return mapper.Map<CinemaModel>(item);
        }
    }
}
