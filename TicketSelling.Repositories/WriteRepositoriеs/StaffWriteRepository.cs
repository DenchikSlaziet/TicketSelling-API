using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Anchors;
using TicketSelling.Repositories.Contracts.WriteRepositoriesContracts;

namespace TicketSelling.Repositories.WriteRepositoriеs
{
    public class StaffWriteRepository : BaseWriteRepository<Staff>, IStaffWriteRepository, IRepositoryAnchor
    {
        public StaffWriteRepository(IDbWriterContext writerContext) 
            : base(writerContext)
        {
             
        }
    }
}
