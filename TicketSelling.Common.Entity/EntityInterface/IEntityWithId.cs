using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSelling.Common.Entity.EntityInterface
{
    public interface IEntityWithId
    {
        public Guid Id { get; set; }
    }
}
