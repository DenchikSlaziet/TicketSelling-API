using AutoMapper;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Repositories.Contracts.WriteRepositoriesContracts;
using TicketSelling.Services.Anchors;
using TicketSelling.Services.Contracts.Exceptions;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ReadServices;

namespace TicketSelling.Services.ReadServices
{
    public class HallService : IHallService, IServiceAnchor
    {
        private readonly IHallWriteRepository hallWriteRepository;
        private readonly IHallReadRepository hallReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public HallService(IHallWriteRepository hallWriteRepository,IHallReadRepository hallReadRepository, IUnitOfWork unitOfWork ,IMapper mapper)
        {
            this.hallWriteRepository = hallWriteRepository;
            this.hallReadRepository = hallReadRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        async Task<HallModel> IHallService.AddAsync(HallModel model, CancellationToken cancellationToken)
        {
            var item = mapper.Map<Hall>(model);

            hallWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<HallModel>(item);
        }

        async Task IHallService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetHall = await hallReadRepository.GetByIdAsync(id, cancellationToken);

            if (targetHall == null)
            {
                throw new TimeTableEntityNotFoundException<Hall>(id);
            }

            hallWriteRepository.Delete(targetHall);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<HallModel> IHallService.EditAsync(HallModel source, CancellationToken cancellationToken)
        {
            var targetHall = await hallReadRepository.GetByIdAsync(source.Id, cancellationToken);

            if (targetHall == null)
            {
                throw new TimeTableEntityNotFoundException<Hall>(source.Id);
            }
            
            targetHall.Number = source.Number;
            targetHall.NumberOfSeats = source.NumberOfSeats;
            hallWriteRepository.Update(targetHall);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<HallModel>(targetHall);
        }

        async Task<IEnumerable<HallModel>> IHallService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await hallReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => mapper.Map<HallModel>(x));
        }

        async Task<HallModel?> IHallService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await hallReadRepository.GetByIdAsync(id, cancellationToken);

            if(item == null)
            {
                throw new TimeTableEntityNotFoundException<Hall>(id);
            

            return mapper.Map<HallModel>(item);
        }
    }
}
