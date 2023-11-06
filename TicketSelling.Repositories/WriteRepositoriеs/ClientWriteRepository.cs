using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Anchors;
using TicketSelling.Repositories.Contracts.WriteRepositoriesContracts;

namespace TicketSelling.Repositories.WriteRepositoriеs
{
    public class ClientWriteRepository : BaseWriteRepository<Client>, IClientWriteRepository, IRepositoryAnchor
    {
        public ClientWriteRepository(IDbWriterContext writerContext) : base(writerContext)
        {
            
        }
    }
}
