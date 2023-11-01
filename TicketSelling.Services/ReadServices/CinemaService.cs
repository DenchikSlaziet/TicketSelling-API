using AutoMapper;
using TicketSelling.Context.Contracts.Interfaces;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Repositories.Contracts.WriteRepositoriesContracts;
using TicketSelling.Services.Anchors;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ReadServices;

namespace TicketSelling.Services.ReadServices
{
    public class CinemaService : ICinemaService, IServiceAnchor
    {
        private readonly ICinemaReadRepository cinemaReadRepositiry;
        private readonly ICinemaWriteRepository cinemaWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CinemaService(ICinemaReadRepository cinemaReadRepositiry, IMapper mapper, ICinemaWriteRepository cinemaWriteRepository, IUnitOfWork unitOfWork)
        {
            this.cinemaReadRepositiry = cinemaReadRepositiry;
            this.mapper = mapper;
            this.cinemaWriteRepository = cinemaWriteRepository;
            this.unitOfWork = unitOfWork;
        }

        async Task ICinemaService.AddCinema(CinemaModel model, CancellationToken cancellationToken)
        {
            var cinema = new Cinema
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Address = model.Address
            };
            cinemaWriteRepository.AddCinema(cinema);
            await unitOfWork.SaveChangesAsync(cancellationToken);
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
