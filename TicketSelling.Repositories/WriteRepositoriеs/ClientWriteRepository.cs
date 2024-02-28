using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Anchors;
using TicketSelling.Repositories.Contracts.WriteRepositoriesContracts;

namespace TicketSelling.Repositories.WriteRepositoriеs
{
    /// <summary>
    /// Реализация <see cref="IClientWriteRepository"/>
    /// </summary>
    public class ClientWriteRepository : BaseWriteRepository<User>, IClientWriteRepository, IRepositoryAnchor
    {
        public ClientWriteRepository(IDbWriterContext writerContext) 
            : base(writerContext)
        {
            
        }
    }
}
