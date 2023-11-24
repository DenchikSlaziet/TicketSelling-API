using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Repositories.Tests
{
    internal static class TestDataGenerator
    {
        static internal Cinema Cinema(Action<Cinema>? settings = null)
        {
            var result = new Cinema
            {           
                Title = $"{Guid.NewGuid():N}",
                Address = $"{Guid.NewGuid():N}"
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static internal Film Film(Action<Film>? settings = null)
        {
            var result = new Film
            {
                Title = $"{Guid.NewGuid():N}",
                Description = $"{Guid.NewGuid():N}"
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static internal Hall Hall(Action<Hall>? settings = null)
        {
            var result = new Hall
            {
                Number = 1,
                NumberOfSeats = 20
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static internal Client Client(Action<Client>? settings = null)
        {
            var result = new Client
            {
                FirstName = $"{Guid.NewGuid():N}",
                LastName = $"{Guid.NewGuid():N}",
                Patronymic = $"{Guid.NewGuid():N}",
                Age = 18,
                Email = $"{Guid.NewGuid():N}"              
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static internal Staff Staff(Action<Staff>? settings = null)
        {
            var result = new Staff
            {
                FirstName = $"{Guid.NewGuid():N}",
                LastName = $"{Guid.NewGuid():N}",
                Patronymic = $"{Guid.NewGuid():N}",
                Age = 18,
                Post = Context.Contracts.Enums.Post.Manager
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static internal Ticket Ticket(Action<Ticket>? settings = null)
        {
            var result = new Ticket
            {
                Date = DateTimeOffset.Now,
                Place = 1,
                Row = 1, 
                Price = 100
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }
    }
}
