using TicketSelling.Common.Entity.InterfaceDB;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Repositories.Anchors;
using TicketSelling.Repositories.Contracts.WriteRepositoriesContracts;

namespace TicketSelling.Repositories.WriteRepositoriеs
{
    /// <summary>
    /// Реализация <see cref="IUserWriteRepository"/>
    /// </summary>
    public class UserWriteRepository : BaseWriteRepository<User>, IUserWriteRepository, IRepositoryAnchor
    {
        public UserWriteRepository(IDbWriterContext writerContext) 
            : base(writerContext)
        {
            
        }
    }
}
