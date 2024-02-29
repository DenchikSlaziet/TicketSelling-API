using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        private readonly Hall hall;
        private readonly User user;
        private readonly Session session;
        private readonly Film film;

        public TicketIntergrationTests(TicketSellingApiFixture fixture) : base(fixture)
        {
            hall = TestDataGenerator.Hall();
            user = TestDataGenerator.User();
            film = TestDataGenerator.Film();
            session = TestDataGenerator.Session(x => { x.FilmId = film.Id; x.HallId = hall.Id; });

            context.Users.Add(user);
            context.Films.Add(film);
            context.Halls.Add(hall);
            context.Sessions.Add(session);
            unitOfWork.SaveChangesAsync();
        }

        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var clientFactory = factory.CreateClient();

            var ticket = mapper.Map<CreateTicketRequest>(TestDataGenerator.TicketRequestModel());
            ticket.UserId = user.Id;
            ticket.SessionId = session.Id;

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

            SetDependenciesOrTicket(ticket);
            await context.Tickets.AddAsync(ticket);
            await unitOfWork.SaveChangesAsync();

            var ticketRequest = mapper.Map<TicketRequest>(TestDataGenerator.TicketRequestModel(x => x.Id = ticket.Id));
            SetDependenciesOrTicketRequestModelWithTicket(ticket, ticketRequest);

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
            var ticket2 = TestDataGenerator.Ticket();

            SetDependenciesOrTicket(ticket1);
            SetDependenciesOrTicket(ticket2);

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
            var ticket2 = TestDataGenerator.Ticket(x => x.DeletedAt = DateTimeOffset.Now);

            SetDependenciesOrTicket(ticket1);
            SetDependenciesOrTicket(ticket2);

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

            SetDependenciesOrTicket(ticket);
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

        private void SetDependenciesOrTicket(Ticket ticket)
        {
            ticket.UserId = user.Id;
            ticket.SessionId = session.Id;
        }

        private void SetDependenciesOrTicketRequestModelWithTicket(Ticket ticket, TicketRequest ticketRequest)
        {
            ticketRequest.SessionId = ticket.SessionId;
            ticketRequest.UserId = ticket.UserId;
        }
    }
}
