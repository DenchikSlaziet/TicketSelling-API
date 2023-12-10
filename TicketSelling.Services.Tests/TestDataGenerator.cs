using TicketSelling.Context.Contracts.Models;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ModelsRequest;

namespace TicketSelling.Services.Tests
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
                Age = 20,
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
                Age = 20,
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

        static internal CinemaModel CinemaModel(Action<CinemaModel>? settings = null)
        {
            var result = new CinemaModel
            {
                Id = Guid.NewGuid(),
                Title = $"{Guid.NewGuid():N}",
                Address = $"{Guid.NewGuid():N}"
            };

            settings?.Invoke(result);
            return result;
        }

        static internal FilmModel FilmModel(Action<FilmModel>? settings = null)
        {
            var result = new FilmModel
            {
                Id = Guid.NewGuid(),
                Title = $"{Guid.NewGuid():N}",
                Description = $"{Guid.NewGuid():N}",
                Limitation = 16
            };

            settings?.Invoke(result);
            return result;
        }

        static internal HallModel HallModel(Action<HallModel>? settings = null)
        {
            var result = new HallModel
            {
                Id = Guid.NewGuid(),
                Number = 1,
                NumberOfSeats = 20
            };

            settings?.Invoke(result);
            return result;
        }

        static internal ClientModel ClientModel(Action<ClientModel>? settings = null)
        {
            var result = new ClientModel
            {
                Id = Guid.NewGuid(),
                FirstName = $"{Guid.NewGuid():N}",
                LastName = $"{Guid.NewGuid():N}",
                Patronymic = $"{Guid.NewGuid():N}",
                Age = 18,
                Email = "kochetkov@gmail.com"
            };

            settings?.Invoke(result);
            return result;
        }

        static internal StaffModel StaffModel(Action<StaffModel>? settings = null)
        {
            var result = new StaffModel
            {
                Id = Guid.NewGuid(),
                FirstName = $"{Guid.NewGuid():N}",
                LastName = $"{Guid.NewGuid():N}",
                Patronymic = $"{Guid.NewGuid():N}",
                Age = 18,
                Post = Contracts.Enums.PostModel.None
            };

            settings?.Invoke(result);
            return result;
        }

        static internal TicketRequestModel TicketRequestModel(Action<TicketRequestModel>? settings = null)
        {
            var result = new TicketRequestModel
            {
                Id = Guid.NewGuid(),
                Date = DateTimeOffset.Now.AddDays(1),
                Place = 1,
                Row = 1,
                Price = 100
            };

            settings?.Invoke(result);
            return result;
        }
    }
}
