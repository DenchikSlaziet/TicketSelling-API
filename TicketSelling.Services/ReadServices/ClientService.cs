using AutoMapper;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Services.Anchors;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ReadServices;

namespace TicketSelling.Services.ReadServices
{
    public class ClientService : IClientService, IServiceAnchor
    {
        private readonly IClientReadRepository clientReadRepository;
        private readonly IMapper mapper;

        public ClientService(IClientReadRepository clientReadRepository, IMapper mapper)
        {
            this.clientReadRepository = clientReadRepository;
            this.mapper = mapper;
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
                return null;
            }
            return mapper.Map<ClientModel>(item);
        }
    }
}
