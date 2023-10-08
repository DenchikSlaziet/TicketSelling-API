using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSelling.Services.AutoMappers;
using Xunit;

namespace TicketSelling.Services.Tests
{
    public class MapperTest
    {
        [Fact]
        public void TestMapper()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ServiceMapper>());
            configuration.AssertConfigurationIsValid();
        }
    }
}
