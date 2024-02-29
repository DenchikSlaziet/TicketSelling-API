using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Anchors;
using TicketSelling.Repositories.Contracts.WriteRepositoriesContracts;

namespace TicketSelling.Repositories.WriteRepositoriеs
{
    public class SessionWriteRepository : BaseWriteRepository<Session>, ISessionWriteRepository, IRepositoryAnchor
    {
        public SessionWriteRepository(IDbWriterContext writerContext) : base(writerContext)
        {
        }
    }
}
