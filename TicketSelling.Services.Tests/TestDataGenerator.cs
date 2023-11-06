using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Services.Tests
{
    static internal class TestDataGenerator
    {
        static internal Cinema Discipline(Action<Cinema>? settings = null)
        {
            var result = new Cinema
            {
                Id = Guid.NewGuid(),
                Title = "Test",
                Address = "Test",
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = $"CreatedBy{Guid.NewGuid():N}",
                UpdatedAt = DateTimeOffset.UtcNow,
                UpdatedBy = $"UpdatedBy{Guid.NewGuid():N}",
            };

            settings?.Invoke(result);
            return result;
        }
    }
}
