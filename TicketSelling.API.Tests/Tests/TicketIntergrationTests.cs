using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Text;
using TicketSelling.API.Models.CreateRequest;
using TicketSelling.API.Models.Response;
using TicketSelling.API.Tests.Infrastructures;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Test.Extensions;
using Xunit;

namespace TicketSelling.API.Tests.Tests
{
    public class TicketIntergrationTests : BaseIntegrationTest
    {
        //private readonly Hall hall;
        //private readonly User user;
        //private readonly Session session;
        //private readonly Film film;

        public TicketIntergrationTests(TicketSellingApiFixture fixture) : base(fixture)
        {
            //var hall = TestDataGenerator.Hall();
            //var user = TestDataGenerator.User();
            //var film = TestDataGenerator.Film();
            //var session = TestDataGenerator.Session();
            //session.FilmId = film.Id;
            //session.HallId = hall.Id;

            //await context.Users.AddAsync(user);
            //await context.Films.AddAsync(film);
            //await context.Halls.AddAsync(hall);
            //await context.Sessions.AddAsync(session);
            //unitOfWork.SaveChangesAsync();
        }

        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var clientFactory = factory.CreateClient();

            var ticket = mapper.Map<CreateTicketRequest>(TestDataGenerator.TicketRequestModel());
            var hall = TestDataGenerator.Hall();
            var user = TestDataGenerator.User();
            var film = TestDataGenerator.Film();
            var session = TestDataGenerator.Session();

            session.FilmId = film.Id;
            session.HallId = hall.Id;
            ticket.UserId = user.Id;
            ticket.SessionId = session.Id;

            await context.Users.AddAsync(user);
            await context.Films.AddAsync(film);
            await context.Halls.AddAsync(hall);
            await context.Sessions.AddAsync(session);
            await unitOfWork.SaveChangesAsync();

            // Act
            string data = JsonConvert.SerializeObject(ticket);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await clientFactory.PostAsync("/Ticket", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TicketResponse>(resultString);

            var cinemaFirst = await context.Tickets.FirstAsync(x => x.Id == result!.Id);

            // Assert          
            cinemaFirst.Should()
                .BeEquivalentTo(ticket);
        }

        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var ticket = TestDataGenerator.Ticket();
            var hall = TestDataGenerator.Hall();
            var user = TestDataGenerator.User();
            var film = TestDataGenerator.Film();
            var session = TestDataGenerator.Session();

            session.FilmId = film.Id;
            session.HallId = hall.Id;
            ticket.SessionId = session.Id;
            ticket.UserId = user.Id;

            await context.Users.AddAsync(user);
            await context.Films.AddAsync(film);
            await context.Halls.AddAsync(hall);
            await context.Sessions.AddAsync(session);
            await context.Tickets.AddAsync(ticket);
            await unitOfWork.SaveChangesAsync();

            var ticketRequest = mapper.Map<TicketRequest>(TestDataGenerator.TicketRequestModel(x => x.Id = ticket.Id));
            ticketRequest.SessionId = session.Id;
            ticketRequest.UserId = user.Id;

            // Act
            string data = JsonConvert.SerializeObject(ticketRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/Ticket", contextdata);

            var ticketFirst = await context.Tickets.FirstAsync(x => x.Id == ticketRequest.Id);

            // Assert           
            ticketFirst.Should()
                .BeEquivalentTo(ticketRequest);
        }

        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var ticket1 = TestDataGenerator.Ticket();
            var hall1 = TestDataGenerator.Hall();
            var user1 = TestDataGenerator.User();
            var film1 = TestDataGenerator.Film();
            var session1 = TestDataGenerator.Session();
            session1.FilmId = film1.Id;
            session1.HallId = hall1.Id;
            ticket1.SessionId = session1.Id;
            ticket1.UserId = user1.Id;

            var ticket2 = TestDataGenerator.Ticket();
            var hall2 = TestDataGenerator.Hall();
            var user2 = TestDataGenerator.User();
            var film2 = TestDataGenerator.Film();
            var session2 = TestDataGenerator.Session();
            session2.FilmId = film2.Id;
            session2.HallId = hall2.Id;
            ticket2.SessionId = session2.Id;
            ticket2.UserId = user2.Id;

            await context.Users.AddAsync(user1);
            await context.Films.AddAsync(film1);
            await context.Halls.AddAsync(hall1);
            await context.Sessions.AddAsync(session1);
            await context.Users.AddAsync(user2);
            await context.Films.AddAsync(film2);
            await context.Halls.AddAsync(hall2);
            await context.Sessions.AddAsync(session2);
            await context.Tickets.AddRangeAsync(ticket1, ticket2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/Ticket/{ticket1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TicketResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    ticket1.Id,
                    ticket1.Place,
                    ticket1.Price,
                    ticket1.Row
                });
        }

        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var ticket1 = TestDataGenerator.Ticket();
            var hall1 = TestDataGenerator.Hall();
            var user1 = TestDataGenerator.User();
            var film1 = TestDataGenerator.Film();
            var session1 = TestDataGenerator.Session();
            session1.FilmId = film1.Id;
            session1.HallId = hall1.Id;
            ticket1.SessionId = session1.Id;
            ticket1.UserId = user1.Id;

            var ticket2 = TestDataGenerator.Ticket(x => x.DeletedAt = DateTimeOffset.Now);
            var hall2 = TestDataGenerator.Hall();
            var user2 = TestDataGenerator.User();
            var film2 = TestDataGenerator.Film();
            var session2 = TestDataGenerator.Session();
            session2.FilmId = film2.Id;
            session2.HallId = hall2.Id;
            ticket2.SessionId = session2.Id;
            ticket2.UserId = user2.Id;

            await context.Users.AddAsync(user1);
            await context.Films.AddAsync(film1);
            await context.Halls.AddAsync(hall1);
            await context.Sessions.AddAsync(session1);
            await context.Users.AddAsync(user2);
            await context.Films.AddAsync(film2);
            await context.Halls.AddAsync(hall2);
            await context.Sessions.AddAsync(session2);
            await context.Tickets.AddRangeAsync(ticket1, ticket2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Ticket");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<TicketResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .Contain(x => x.Id == ticket1.Id)
                .And
                .NotContain(x => x.Id == ticket2.Id);
        }

        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var ticket = TestDataGenerator.Ticket();
            var hall = TestDataGenerator.Hall();
            var user = TestDataGenerator.User();
            var film = TestDataGenerator.Film();
            var session = TestDataGenerator.Session();

            session.FilmId = film.Id;
            session.HallId = hall.Id;
            ticket.SessionId = session.Id;
            ticket.UserId = user.Id;

            await context.Users.AddAsync(user);
            await context.Films.AddAsync(film);
            await context.Halls.AddAsync(hall);
            await context.Sessions.AddAsync(session);
            await context.Tickets.AddAsync(ticket);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/Ticket/{ticket.Id}");

            var ticketFirst = await context.Tickets.FirstAsync(x => x.Id == ticket.Id);

            // Assert
            ticketFirst.DeletedAt.Should()
                .NotBeNull();

            ticketFirst.Should()
                .BeEquivalentTo(new
                {
                    ticket.DatePayment,
                    ticket.Place,
                    ticket.Price,
                    ticket.Row
                });
        }      
    }
}
