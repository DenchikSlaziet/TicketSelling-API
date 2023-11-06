using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Anchors;
using TicketSelling.Repositories.Contracts.WriteRepositoriesContracts;

namespace TicketSelling.Repositories.WriteRepositoriеs
{
    public class HallWriteRepository : BaseWriteRepository<Hall>, IHallWriteRepository, IRepositoryAnchor
    {
        public HallWriteRepository(IDbWriterContext writerContext) 
            : base(writerContext)
        {
            
        }
    }
}
