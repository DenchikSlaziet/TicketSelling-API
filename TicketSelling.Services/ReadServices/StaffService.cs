using AutoMapper;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Services.Anchors;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ReadServices;

namespace TicketSelling.Services.ReadServices
{
    public class StaffService : IStaffService, IServiceAnchor
    {
        private readonly IStaffReadRepository staffReadRepository;
        private readonly IMapper mapper;

        public StaffService(IStaffReadRepository staffReadRepository, IMapper mapper)
        {
            this.staffReadRepository = staffReadRepository;
            this.mapper = mapper;
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
