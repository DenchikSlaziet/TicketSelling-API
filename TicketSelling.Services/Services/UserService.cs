using AutoMapper;
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
    /// <inheritdoc cref="IUserService"/>
    public class UserService : IUserService, IServiceAnchor
    {
        private readonly IUserWriteRepository clientWriteRepository;
        private readonly IUserReadRepository clientReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidatorService validatorService;

        public UserService(IUserWriteRepository clientWriteRepository, IUserReadRepository clientReadRepository, 
            IUnitOfWork unitOfWork, IMapper mapper, IServiceValidatorService validatorService)
        {
            this.clientReadRepository = clientReadRepository;
            this.clientWriteRepository = clientWriteRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.validatorService = validatorService;
        }

        async Task<UserModel> IUserService.AddAsync(UserModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();
            await validatorService.ValidateAsync(model, cancellationToken);

            var item = mapper.Map<User>(model);

            clientWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<UserModel>(item);
        }

        async Task IUserService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetClient = await clientReadRepository.GetNotDeletedByIdAsync(id, cancellationToken);

            if (targetClient == null)
            {
                throw new TicketSellingEntityNotFoundException<User>(id);
            }

            clientWriteRepository.Delete(targetClient);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<UserModel> IUserService.EditAsync(UserModel source, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(source, cancellationToken);

            var targetClient = await clientReadRepository.GetNotDeletedByIdAsync(source.Id, cancellationToken);

            if (targetClient == null)
            {
                throw new TicketSellingEntityNotFoundException<User>(source.Id);
            }

            var times = new { targetClient.CreatedAt, targetClient.CreatedBy };
            targetClient = mapper.Map<User>(source);
            targetClient.CreatedAt = times.CreatedAt;
            targetClient.CreatedBy = times.CreatedBy;

            clientWriteRepository.Update(targetClient);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<UserModel>(targetClient);
        }

        async Task<IEnumerable<UserModel>> IUserService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await clientReadRepository.GetAllAsync(cancellationToken);
            return result.Select(x => mapper.Map<UserModel>(x));
        }

        async Task<UserModel?> IUserService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await clientReadRepository.GetNotDeletedByIdAsync(id, cancellationToken);

            if(item == null) 
            {
                throw new TicketSellingEntityNotFoundException<User>(id);
            }

            return mapper.Map<UserModel>(item);
        }
    }
}
