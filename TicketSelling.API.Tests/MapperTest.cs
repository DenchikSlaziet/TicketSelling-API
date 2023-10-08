using AutoMapper;
using TicketSelling.API.AutoMappers;
using Xunit;

namespace TicketSelling.API.Tests
{
    public class MapperTest
    {
        [Fact]
        public void TestMapper()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<APIMappers>());
            configuration.AssertConfigurationIsValid();
        }
    }
}