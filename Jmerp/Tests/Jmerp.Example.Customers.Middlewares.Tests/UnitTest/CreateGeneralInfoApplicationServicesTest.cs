using NUnit.Framework;
using FluentAssertions;
using System.Threading.Tasks;
using Jmerp.Example.Customers.Middlewares.Tests.Mocks;
using System.Linq;
using Jmerp.Example.Customers.Middlewares.Models;
using Jmerp.Commons;
using System.Collections.Generic;
using System.Threading;
using Jmerp.Example.Customers.Middlewares.Services;
using EventFlow.Configuration;
using EventFlow;
using EventFlow.Extensions;
using EventFlow.Logs;

namespace Jmerp.Example.Customers.Middlewares.Tests.UnitTest
{
    [Category("Unit Test")]
    public class CreateGeneralInfoApplicationServicesTest
    {
        private IRootResolver _resolver;

        [SetUp]
        public void SetUp()
        {
            _resolver = EventFlowOptions.New
                .CustomerBootstrapperConfiguration()
                .CreateResolver();
        }

        [TearDown]
        public void TearDown()
        {
            _resolver.DisposeSafe(new ConsoleLog(), "");
        }

        [Test]
        public async Task Simple()
        {
            //Arrange
            var customer = CustomerDtoList.Customer_CS00001;

            //Act
            var services = _resolver.Resolve<ICreateGeneralInfoApplicationServices>();
            var response = await services.CreateAsync(customer, CancellationToken.None);

            var responseResult = ConvertResponse(response.Responses)?.ToList();

            //Assert
            response.Succeeded.Should().BeTrue();
            response.Errors.Should().HaveCount(0);
            responseResult.Should().BeOfType(typeof(List<CustomerDto>));
        }

        private IEnumerable<CustomerDto> ConvertResponse(IEnumerable<object> objects)
        {
            foreach (var item in objects)
            {
                yield return (CustomerDto)item;
            }
        }
    }
}
