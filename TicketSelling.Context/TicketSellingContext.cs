using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TicketSelling.Context.Contracts;
using TicketSelling.Context.Contracts.Models;

namespace TicketSelling.Context
{
    public class TicketSellingContext : ITicketSellingContext
    {
        private readonly List<Cinema> cinemas;
        private readonly List<Ticket> tickets;
        private readonly List<Client> clients;
        private readonly List<Film> films;
        private readonly List<Hall> halls;
        private readonly List<Staff> staffs;

        public TicketSellingContext()
        {
            cinemas = new List<Cinema>()
            {
                new Cinema
                {
                    Id = Guid.NewGuid(),
                    Address = "ПКГХ",
                    Title = "СиниемаГолд"
                }
            };

            films = new List<Film>()
            {
                new Film
                {
                    Id = Guid.NewGuid(),
                    Description = "Фильм говно",
                    Limitation = 12,
                    Title = "Барби"
                }
            };
            clients = new List<Client>()
            {
                new Client
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Денис",
                    LastName = "Кочетков",
                    Patronymic = "Александрович",
                    Age = 19,
                    Email = "fdsffasas"
                }
            };

            halls = new List<Hall>()
            {
                new Hall
                {
                    Id = Guid.NewGuid(),
                    Number = 1,
                    NumberOfSeats = 12
                }
            };

            staffs = new List<Staff>()
            {
                new Staff
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Денис",
                    LastName = "Кочетков",
                    Patronymic = "Александрович",
                    Age = 19,
                    Post = Contracts.Enums.Post.Manager
                }
            };
            tickets = new List<Ticket>();
        }

        IEnumerable<Cinema> ITicketSellingContext.Cinemas => cinemas;

        IEnumerable<Ticket> ITicketSellingContext.Tickets => tickets;

        IEnumerable<Client> ITicketSellingContext.Clients => clients;

        IEnumerable<Film> ITicketSellingContext.Films => films;

        IEnumerable<Hall> ITicketSellingContext.Halls => halls;

        IEnumerable<Staff> ITicketSellingContext.Staffs => staffs;
    }
}
