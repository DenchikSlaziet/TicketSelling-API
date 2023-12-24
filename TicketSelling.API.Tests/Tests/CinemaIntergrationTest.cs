using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using TicketSelling.API.Models.CreateRequest;
using TicketSelling.API.Models.Response;
using TicketSelling.API.Tests.Infrastructures;
using TicketSelling.Test.Extensions;
using Xunit;

namespace TicketSelling.API.Tests.Tests
{
    public class CinemaIntergrationTest : BaseIntegrationTest
    {
        public CinemaIntergrationTest(TicketSellingApiFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var cinema = mapper.Map<CreateCinemaRequest>(TestDataGenerator.CinemaModel());

            // Act
            string data = JsonConvert.SerializeObject(cinema);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Cinema", contextdata);

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<CinemaResponse>(resultString);
            result.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    cinema.Title,
                    cinema.Address
                });          
        }

        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var cinema = TestDataGenerator.Cinema();
            await context.Cinemas.AddAsync(cinema);
            await unitOfWork.SaveChangesAsync();

            var cinemaRequest = mapper.Map<CinemaRequest>(TestDataGenerator.CinemaModel(x => x.Id = cinema.Id));

            // Act
            string data = JsonConvert.SerializeObject(cinemaRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("/Cinema", contextdata);

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<CinemaResponse>(resultString);
            result.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    cinema.Id
                })
                .And
                .NotBeEquivalentTo(new
                {
                    cinema.Title,
                    cinema.Address
                });
        }

        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var cinema = TestDataGenerator.Cinema();
            await context.Cinemas.AddAsync(cinema);
            await unitOfWork.SaveChangesAsync();
           
            // Act
            await client.DeleteAsync($"/Cinema/{cinema.Id}");

            // Assert
            context.Cinemas.Should().ContainSingle(x => x.Id == cinema.Id && x.DeletedAt != null);
        }

        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var cinema1 = TestDataGenerator.Cinema();
            var cinema2 = TestDataGenerator.Cinema();

            await context.Cinemas.AddRangeAsync(cinema1,cinema2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/Cinema/{cinema1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<CinemaResponse>(resultString);

            result.Should().NotBeNull()
                .And
                .BeEquivalentTo(new 
                {
                    cinema1.Title,
                    cinema1.Address
                });
        }

        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var cinema1 = TestDataGenerator.Cinema();
            var cinema2 = TestDataGenerator.Cinema(x=> x.DeletedAt = DateTimeOffset.Now);

            await context.Cinemas.AddRangeAsync(cinema1,cinema2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Cinema");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<CinemaResponse>>(resultString);
            result.Should()
                .NotBeNull()
                .And
                .ContainSingle(x => x.Id == cinema1.Id);
        }
    }
}
