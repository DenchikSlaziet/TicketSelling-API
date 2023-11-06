using AutoMapper;
using TicketSelling.Common.Entity.InterfaceDB;
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

        public CinemaService(ICinemaReadRepository cinemaReadRepositiry, IMapper mapper, 
            ICinemaWriteRepository cinemaWriteRepository, IUnitOfWork unitOfWork)
        {
            this.cinemaReadRepositiry = cinemaReadRepositiry;
            this.mapper = mapper;
            this.cinemaWriteRepository = cinemaWriteRepository;
            this.unitOfWork = unitOfWork;
        }

        async Task<CinemaModel> ICinemaService.AddAsync(string address, string title, CancellationToken cancellationToken)
        {
            var item = new Cinema
            {
                Id = Guid.NewGuid(),
                Address = address,
                Title = title,
            };

            cinemaWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<CinemaModel>(item);
        }

        Task ICinemaService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<CinemaModel> ICinemaService.EditAsync(CinemaModel source, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
