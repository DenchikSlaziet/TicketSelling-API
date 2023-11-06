using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSelling.Common.Entity.InterfaceDB
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Асинхронно сохраняет все изменения в бд
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
