﻿using AutoMapper;
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
    public class StaffService : IStaffService, IServiceAnchor
    {
        private readonly IStaffWriteRepository staffWriteRepository;
        private readonly IStaffReadRepository staffReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public StaffService(IStaffWriteRepository staffWriteRepository, IStaffReadRepository staffReadRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.staffWriteRepository = staffWriteRepository;
            this.staffReadRepository = staffReadRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<StaffModel> IStaffService.AddAsync(string firstName, string lastName, string patronymic, 
            short age, int post, CancellationToken cancellationToken)
        {
            var item = new Staff
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Patronymic = patronymic,
                Age = age,
                Post = (Post)post
            };

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
            var targetStaff = await staffReadRepository.GetByIdAsync(source.Id, cancellationToken);

            if (targetStaff == null)
            {
                throw new TimeTableEntityNotFoundException<Staff>(source.Id);
            }

            targetStaff.FirstName = source.FirstName;
            targetStaff.LastName = source.LastName;
            targetStaff.Patronymic = source.Patronymic;
            targetStaff.Post = (Post)source.Post;
            targetStaff.Age = source.Age;

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

            if(item == null)
            {
                return null;
            }
            return mapper.Map<StaffModel>(item);
        }
    }
}