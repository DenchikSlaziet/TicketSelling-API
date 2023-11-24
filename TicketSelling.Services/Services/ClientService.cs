using AutoMapper;
using FluentValidation;
using TicketSelling.API.Validation.Validators;
using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Repositories.Contracts.WriteRepositoriesContracts;
using TicketSelling.Services.Anchors;
using TicketSelling.Services.Contracts.Exceptions;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ReadServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace TicketSelling.Services.ReadServices
{
    public class ClientService : IClientService, IServiceAnchor
    {
        private readonly IClientWriteRepository clientWriteRepository;
        private readonly IClientReadRepository clientReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly CreateClientRequestValidator validations;

        public ClientService(IClientWriteRepository clientWriteRepository, IClientReadRepository clientReadRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.clientReadRepository = clientReadRepository;
            this.clientWriteRepository = clientWriteRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            validations = new CreateClientRequestValidator();
        }

        async Task<ClientModel> IClientService.AddAsync(ClientModel model, CancellationToken cancellationToken)
        {
            await validations.ValidateAndThrowAsync(model, cancellationToken);

            var item = mapper.Map<Client>(model);

            clientWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ClientModel>(item);
        }

        async Task IClientService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetClient = await clientReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetClient == null)
            {
                throw new TimeTableEntityNotFoundException<Client>(id);
            }

            if (targetClient.DeletedAt.HasValue)
            {
                throw new TimeTableInvalidOperationException($"Клиент с идентификатором {id} уже удален");
            }

            clientWriteRepository.Delete(targetClient);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<ClientModel> IClientService.EditAsync(ClientModel source, CancellationToken cancellationToken)
        {
            await validations.ValidateAndThrowAsync(source, cancellationToken);

            var targetClient = await clientReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetClient == null)
            {
                throw new TimeTableEntityNotFoundException<Client>(source.Id);
            }

            targetClient.FirstName = source.FirstName;
            targetClient.LastName = source.LastName;
            targetClient.Patronymic = source.Patronymic;
            targetClient.Age = source.Age;
            targetClient.Email = source.Email;

            clientWriteRepository.Update(targetClient);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ClientModel>(targetClient);
        }

        async Task<IEnumerable<ClientModel>> IClientService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await clientReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => mapper.Map<ClientModel>(x));
        }

        async Task<ClientModel?> IClientService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await clientReadRepository.GetByIdAsync(id, cancellationToken);

            if(item == null) 
            {
                throw new TimeTableEntityNotFoundException<Client>(id);
            }

            return mapper.Map<ClientModel>(item);
        }
    }
}
