using TicketSelling.Context.Contracts.Models;
using TicketSelling.Services.Contracts.Enums;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ModelsRequest;

namespace TicketSelling.Test.Extensions
{
    public static class TestDataGenerator
    {

        private static Random random = new Random();
        
        static public Session Session(Action<Session>? settings = null)
        {
            var result = new Session
            {
                StartDateTime = DateTimeOffset.Now.AddDays(2),
                EndDateTime = DateTimeOffset.Now.AddDays(2).AddHours(2)
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static public Film Film(Action<Film>? settings = null)
        {
            var result = new Film
            {
                Title = $"{Guid.NewGuid():N}",
                Description = $"{Guid.NewGuid():N}",
                Genre = Context.Contracts.Enums.Genre.War
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static public Hall Hall(Action<Hall>? settings = null)
        {
            var result = new Hall
            {
                Number = random.Next(1,900),
                CountPlaceInRow= random.Next(3,11),
                CountRow = random.Next(1, 8)
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static public User User(Action<User>? settings = null)
        {
            var result = new User
            {
                FirstName = $"{Guid.NewGuid():N}",
                LastName = $"{Guid.NewGuid():N}",
                Patronymic = $"{Guid.NewGuid():N}",
                Age = random.Next(1, 90),
                Email = $"{Guid.NewGuid():N}@gmail.com",
                Login = $"{Guid.NewGuid():N}",
                Password = string.Join("", Guid.NewGuid().ToString().Take(10)),
                Role = Context.Contracts.Enums.Role.Quest
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static public Staff Staff(Action<Staff>? settings = null)
        {
            var result = new Staff
            {
                FirstName = $"{Guid.NewGuid():N}",
                LastName = $"{Guid.NewGuid():N}",
                Patronymic = $"{Guid.NewGuid():N}",
                Age = random.Next(19, 90),
                Post = Context.Contracts.Enums.Post.None
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static public Ticket Ticket(Action<Ticket>? settings = null)
        {
            var result = new Ticket
            {
                DatePayment = DateTimeOffset.Now,
                Place = random.Next(1, 11),
                Row = random.Next(1, 8),
                Price = random.Next(100, 4500)
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static public FilmModel FilmModel(Action<FilmModel>? settings = null)
        {
            var result = new FilmModel
            {
                Id = Guid.NewGuid(),
                Title = $"{Guid.NewGuid():N}",
                Description = $"{Guid.NewGuid():N}",
                Limitation = 16,
                Genre = GenreModel.War
            };

            settings?.Invoke(result);
            return result;
        }

        static public HallModel HallModel(Action<HallModel>? settings = null)
        {
            var result = new HallModel
            {
                Id = Guid.NewGuid(),
                Number = random.Next(1,1000),
                CountPlaceInRow = random.Next(3, 11),
                CountRow = random.Next(1, 8)
            };

            settings?.Invoke(result);
            return result;
        }

        static public UserModel ClientModel(Action<UserModel>? settings = null)
        {
            var result = new UserModel
            {
                Id = Guid.NewGuid(),
                FirstName = $"{Guid.NewGuid():N}",
                LastName = $"{Guid.NewGuid():N}",
                Patronymic = $"{Guid.NewGuid():N}",
                Age = random.Next(1, 90),
                Email = $"{Guid.NewGuid():N}@gmail.com",
                Login = $"{Guid.NewGuid():N}",
                Password = string.Join("",Guid.NewGuid().ToString().Take(10)),
                Role = RoleModel.Quest
            };

            settings?.Invoke(result);
            return result;
        }

        static public StaffModel StaffModel(Action<StaffModel>? settings = null)
        {
            var result = new StaffModel
            {
                Id = Guid.NewGuid(),
                FirstName = $"{Guid.NewGuid():N}",
                LastName = $"{Guid.NewGuid():N}",
                Patronymic = $"{Guid.NewGuid():N}",
                Age = random.Next(19, 90),
                Post = PostModel.None               
            };

            settings?.Invoke(result);
            return result;
        }

        static public TicketRequestModel TicketRequestModel(Action<TicketRequestModel>? settings = null)
        {
            var result = new TicketRequestModel
            {
                Id = Guid.NewGuid(),
                DatePayment = DateTimeOffset.Now,
                Place = random.Next(1, 10),
                Row = random.Next(1, 7),
                Price = random.Next(500, 4500)               
            };

            settings?.Invoke(result);
            return result;
        }
        static public SessionRequestModel SessionRequestModel(Action<SessionRequestModel>? settings = null)
        {
            var result = new SessionRequestModel
            {
                Id = Guid.NewGuid(),
                StartDateTime = DateTimeOffset.Now.AddHours(6),
                EndDateTime = DateTimeOffset.Now.AddHours(8)
            };

            settings?.Invoke(result);
            return result;
        }
    }
}
