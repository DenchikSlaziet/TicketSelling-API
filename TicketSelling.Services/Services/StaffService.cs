﻿using AutoMapper;
using FluentValidation;
using TicketSelling.API.Validation.Validators;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Context.Contracts.Enums;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Repositories.Contracts.WriteRepositoriesContracts;
using TicketSelling.Services.Anchors;
using TicketSelling.Services.Contracts.Exceptions;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ReadServices;

namespace TicketSelling.Services.ReadServices
{
    /// <inheritdoc cref="IStaffService"/>
    public class StaffService : IStaffService, IServiceAnchor
    {
        private readonly IStaffWriteRepository staffWriteRepository;
        private readonly IStaffReadRepository staffReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly CreateStaffRequestValidator validator;

        public StaffService(IStaffWriteRepository staffWriteRepository, IStaffReadRepository staffReadRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.staffWriteRepository = staffWriteRepository;
            this.staffReadRepository = staffReadRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            validator = new CreateStaffRequestValidator();
        }

        async Task<StaffModel> IStaffService.AddAsync(StaffModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();
            await validator.ValidateAndThrowAsync(model, cancellationToken);

            var item = mapper.Map<Staff>(model);

            staffWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<StaffModel>(item);
        }

        async Task IStaffService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetStaff = await staffReadRepository.GetByIdAsync(id, cancellationToken);

            if (targetStaff == null)
            {
                throw new TimeTableEntityNotFoundException<Staff>(id);
            }

            if (targetStaff.DeletedAt.HasValue)
            {
                throw new TimeTableInvalidOperationException($"Работник с идентификатором {id} уже удален");
            }

            staffWriteRepository.Delete(targetStaff);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<StaffModel> IStaffService.EditAsync(StaffModel source, CancellationToken cancellationToken)
        {
            await validator.ValidateAndThrowAsync(source, cancellationToken);

            var targetStaff = await staffReadRepository.GetByIdAsync(source.Id, cancellationToken);

            if (targetStaff == null)
            {
                throw new TimeTableEntityNotFoundException<Staff>(source.Id);
            }

            targetStaff = mapper.Map<Staff>(source);

            staffWriteRepository.Update(targetStaff);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<StaffModel>(targetStaff);
        }

        async Task<IEnumerable<StaffModel>> IStaffService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await staffReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => mapper.Map<StaffModel>(x));
        }

        async Task<StaffModel?> IStaffService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await staffReadRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                throw new TimeTableEntityNotFoundException<Staff>(id);
            }

            return mapper.Map<StaffModel>(item);
        }
    }
}
