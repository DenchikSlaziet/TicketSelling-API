using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSelling.Common.Entity.InterfaceDB
{
    public interface IDbWriterContext
    {
        /// <inheritdoc cref="IDbWriter"/>
        IDbWriter Writer { get; }


        /// <inheritdoc cref="IUnitOfWork"/>
        IUnitOfWork UnitOfWork { get; }

    }
}
