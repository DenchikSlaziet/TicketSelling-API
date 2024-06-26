﻿using AutoMapper;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Repositories.Contracts.WriteRepositoriesContracts;
using TicketSelling.Services.Anchors;
using TicketSelling.Services.Contracts.Exceptions;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ServicesContracts;

namespace TicketSelling.Services.ReadServices
{
    /// <inheritdoc cref="IHallService"/>
    public class HallService : IHallService, IServiceAnchor
    {
        private readonly IHallWriteRepository hallWriteRepository;
        private readonly IHallReadRepository hallReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidatorService validatorService;

        public HallService(IHallWriteRepository hallWriteRepository,IHallReadRepository hallReadRepository, 
            IUnitOfWork unitOfWork, IMapper mapper, IServiceValidatorService validatorService)
        {
            this.hallWriteRepository = hallWriteRepository;
            this.hallReadRepository = hallReadRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.validatorService = validatorService;
        }

        async Task<HallModel> IHallService.AddAsync(HallModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();
            await validatorService.ValidateAsync(model, cancellationToken);

            var item = mapper.Map<Hall>(model);

            hallWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<HallModel>(item);
        }

        async Task IHallService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetHall = await hallReadRepository.GetNotDeletedByIdAsync(id, cancellationToken);

            if (targetHall == null)
            {
                throw new TicketSellingEntityNotFoundException<Hall>(id);
            }

            hallWriteRepository.Delete(targetHall);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<HallModel> IHallService.EditAsync(HallModel source, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(source, cancellationToken);

            var targetHall = await hallReadRepository.GetNotDeletedByIdAsync(source.Id, cancellationToken);

            if (targetHall == null)
            {
                throw new TicketSellingEntityNotFoundException<Hall>(source.Id);
            }

            var times = new { targetHall.CreatedAt, targetHall.CreatedBy };
            targetHall = mapper.Map<Hall>(source);
            targetHall.CreatedAt = times.CreatedAt;
            targetHall.CreatedBy = times.CreatedBy;

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
            var item = await hallReadRepository.GetNotDeletedByIdAsync(id, cancellationToken);

            if (item == null)
            {
                throw new TicketSellingEntityNotFoundException<Hall>(id);
            }
            
            return mapper.Map<HallModel>(item);
        }
    }
}
