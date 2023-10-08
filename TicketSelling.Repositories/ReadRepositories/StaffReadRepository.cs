using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Context.Contracts;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Contracts.ReadInterfaces;

namespace TicketSelling.Repositories.ReadRepositories
{
    /// <summary>
    /// Реализация <see cref="IStaffReadRepository"/>
    /// </summary>
    public class StaffReadRepository : IStaffReadRepository
    {
        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private ITicketSellingContext context;

        public StaffReadRepository(ITicketSellingContext context)
        {
            this.context = context;
        }
        Task<List<Staff>> IStaffReadRepository.GetAllAsync(CancellationToken cancellationToken) 
            => Task.FromResult(context.Staffs.ToList());

        Task<Staff?> IStaffReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken) 
            => Task.FromResult(context.Staffs.FirstOrDefault(x => x.Id == id));

        Task<Dictionary<Guid, Staff>> IStaffReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken) 
            => Task.FromResult(context.Staffs.Where(x => ids.Contains(x.Id)).ToDictionary(x => x.Id));
    }
}
