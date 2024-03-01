using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using TicketSelling.API.Models.CreateRequest;
using TicketSelling.API.Models.Request;
using TicketSelling.API.Models.Response;
using TicketSelling.API.Tests.Infrastructures;
using TicketSelling.Context.Contracts.Models;
using TicketSelling.Test.Extensions;
using Xunit;

namespace TicketSelling.API.Tests.Tests
{
    public class SessionIntegrationTests : BaseIntegrationTest
    {
        public SessionIntegrationTests(TicketSellingApiFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var session = mapper.Map<CreateSessionRequest>(TestDataGenerator.SessionRequestModel());
            var hall = TestDataGenerator.Hall();
            var film = TestDataGenerator.Film();

            session.HallId = hall.Id;
            session.FilmId = film.Id;

            await context.Films.AddAsync(film);
            await context.Halls.AddAsync(hall);
            await unitOfWork.SaveChangesAsync();

            // Act
            string data = JsonConvert.SerializeObject(session);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Session", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SessionResponse>(resultString);

            var sessionFirst = await context.Sessions.FirstAsync(x => x.Id == result!.Id);

            // Assert          
            sessionFirst.Should()
                .BeEquivalentTo(session);
        }

        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var session = TestDataGenerator.Session();
            var hall = TestDataGenerator.Hall();
            var film = TestDataGenerator.Film();

            session.FilmId = film.Id;
            session.HallId = hall.Id;

            await context.Films.AddAsync(film);
            await context.Halls.AddAsync(hall);
            await context.Sessions.AddAsync(session);
            await unitOfWork.SaveChangesAsync();

            var sessionRequest = mapper.Map<SessionRequest>(TestDataGenerator.SessionRequestModel(x => x.Id = session.Id));
            sessionRequest.FilmId = session.FilmId;
            sessionRequest.HallId = session.HallId;

            // Act
            string data = JsonConvert.SerializeObject(sessionRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/Session", contextdata);

            var sessionFirst = await context.Sessions.FirstAsync(x => x.Id == session.Id);

            // Assert           
            sessionFirst.Should()
                .BeEquivalentTo(sessionRequest);
        }

        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var session = TestDataGenerator.Session();
            var hall = TestDataGenerator.Hall();
            var film = TestDataGenerator.Film();

            session.FilmId = film.Id;
            session.HallId = hall.Id;

            await context.Films.AddAsync(film);
            await context.Halls.AddAsync(hall);
            await context.Sessions.AddAsync(session);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/Session/{session.Id}");

            var sessionFirst = await context.Sessions.FirstAsync(x => x.Id == session.Id);

            // Assert
            sessionFirst.DeletedAt.Should()
                .NotBeNull();

            sessionFirst.Should()
            .BeEquivalentTo(new
            {
                session.Id,
                session.FilmId,
                session.HallId,
                session.StartDateTime,
                session.EndDateTime
            });
        }

        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var session = TestDataGenerator.Session();
            var hall = TestDataGenerator.Hall();
            var film = TestDataGenerator.Film();

            session.FilmId = film.Id;
            session.HallId = hall.Id;

            await context.Films.AddAsync(film);
            await context.Halls.AddAsync(hall);
            await context.Sessions.AddAsync(session);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/Session/{session.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SessionResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    session.Id,
                    session.StartDateTime,
                    session.EndDateTime
                });
        }

        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var session1 = TestDataGenerator.Session();
            var hall = TestDataGenerator.Hall();
            var film = TestDataGenerator.Film();

            session1.FilmId = film.Id;
            session1.HallId = hall.Id;

            var session2 = TestDataGenerator.Session(x => x.DeletedAt = DateTimeOffset.Now);
            session2.FilmId = film.Id;
            session2.HallId = hall.Id;

            await context.Films.AddAsync(film);
            await context.Halls.AddAsync(hall);
            await context.Sessions.AddRangeAsync(session1, session2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Session");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<SessionResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .Contain(x => x.Id == session1.Id);

            result.Should()
                .NotBeNull()
                .And
                .NotContain(x => x.Id == session2.Id);
        }
    }
}
