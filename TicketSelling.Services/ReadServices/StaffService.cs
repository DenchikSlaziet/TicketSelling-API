using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Repositories.Contracts.ReadInterfaces;
using TicketSelling.Services.Contracts.Enums;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ReadServices;

namespace TicketSelling.Services.ReadServices
{
    public class StaffService : IStaffService
    {
        private readonly IStaffReadRepository staffReadRepository;

        public StaffService(IStaffReadRepository staffReadRepository)
        {
            this.staffReadRepository = staffReadRepository;
        }

        async Task<IEnumerable<StaffModel>> IStaffService.GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await staffReadRepository.GetAllAsync(cancellationToken);

            return result.Select(x => new StaffModel
            {
                Id = x.Id,
                Age = x.Age,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Patronymic = x.Patronymic
                //Post = (Post)x.Post
            });
        }

        async Task<StaffModel?> IStaffService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await staffReadRepository.GetByIdAsync(id, cancellationToken);

            if(item == null)
            {
                return null;
            }

            return new StaffModel
            {
                Id = item.Id,
                Age = item.Age,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Patronymic = item.Patronymic
                //Post = (Post)item.Post
            };
        }
    }
}
