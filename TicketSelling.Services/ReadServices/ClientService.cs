using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ReadServices;

namespace TicketSelling.Services.ReadServices
{
    public class ClientService : IClientService
    {
        private readonly IClientReadRepository clientReadRepository;

        public ClientService(IClientReadRepository clientReadRepository)
        {
            this.clientReadRepository = clientReadRepository;
        }

        async Task<IEnumerable<ClientModel>> IClientService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await clientReadRepository.GetAllAsync(cancellationToken);

            return result.Select(x => new ClientModel
            {
                Id = x.Id,
                Age = x.Age,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Patronymic = x.Patronymic
            });
        }

        async Task<ClientModel?> IClientService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await clientReadRepository.GetByIdAsync(id, cancellationToken);

            if(item == null) 
            {
                return null;
            }

            return new ClientModel
            {
                Id = item.Id,
                Age = item.Age,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Patronymic = item.Patronymic
            };
        }
    }
}
