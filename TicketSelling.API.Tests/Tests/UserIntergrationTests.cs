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
    /// <summary>
    /// Переделаю
    /// </summary>
    public class UserIntergrationTests : BaseIntegrationTest
    {
        public UserIntergrationTests(TicketSellingApiFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var clientFactory = factory.CreateClient();
            var client = mapper.Map<CreateUserRequest>(TestDataGenerator.ClientModel());

            // Act
            string data = JsonConvert.SerializeObject(client);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await clientFactory.PostAsync("/User", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UserResponse>(resultString);

            var clientFirst = await context.Users.FirstAsync(x => x.Id == result!.Id);

            // Assert          
            clientFirst.Should()
                .BeEquivalentTo(client);
        }

        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var clientFactory = factory.CreateClient();
            var client = TestDataGenerator.User();
            await context.Users.AddAsync(client);
            await unitOfWork.SaveChangesAsync();

            var clientRequest = mapper.Map<UserRequest>(TestDataGenerator.ClientModel(x =>  x.Id = client.Id));

            // Act
            string data = JsonConvert.SerializeObject(clientRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await clientFactory.PutAsync("/User", contextdata);

            var clientFirst = await context.Users.FirstAsync(x => x.Id == clientRequest.Id);

            // Assert           
            clientFirst.Should()
                .BeEquivalentTo(clientRequest);
        }

        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var clientFactory = factory.CreateClient();
            var client = TestDataGenerator.User();
            await context.Users.AddAsync(client);
            await unitOfWork.SaveChangesAsync();

            // Act
            await clientFactory.DeleteAsync($"/User/{client.Id}");

            var clientFirst = await context.Users.FirstAsync(x => x.Id == client.Id);

            // Assert
            clientFirst.DeletedAt.Should()
                .NotBeNull();

            clientFirst.Should()
            .BeEquivalentTo(new
                {
                    client.Age,
                    client.Id,
                    client.Login,
                    client.Password
            });
        }

        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var clientFactory = factory.CreateClient();
            var client1 = TestDataGenerator.User();
            var client2 = TestDataGenerator.User();

            await context.Users.AddRangeAsync(client1, client2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await clientFactory.GetAsync($"/User/{client1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UserResponse>(resultString);

            result.Should()
                .BeEquivalentTo(new 
                {
                    client1.Id,
                    client1.Age,
                    client1.Login,
                    client1.Password
                });
        }

        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var clientFactory = factory.CreateClient();
            var client1 = TestDataGenerator.User();
            var client2 = TestDataGenerator.User(x => x.DeletedAt = DateTimeOffset.Now);

            await context.Users.AddRangeAsync(client1, client2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await clientFactory.GetAsync("/User");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<UserResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .Contain(x => x.Id == client1.Id);

            result.Should()
                .NotBeNull()
                .And
                .NotContain(x => x.Id == client2.Id);
        }
    }
}
