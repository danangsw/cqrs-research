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
using Autofac;
using Jmerp.Middlewares.Boostrappers;

namespace Jmerp.Example.Customers.Middlewares.Tests.IntegrationTests
{
    [Category("Web Integration Test")]
    public class Scenarios
    {
        private ContainerBuilder _containerBuilder;
        private IContainer _container;

        [SetUp]
        public void Setup()
        {
            _containerBuilder = new ContainerBuilder();
            _container = _containerBuilder.MiddlewaresStartUp();
        }

        [TearDown]
        public void TearDown()
        {
            _container.Dispose();
        }

        [Test]
        public async Task Simple()
        {
            //Arrange
            var customer = CustomerDtoList.Customer_CS00001;

            var services =  _container.Resolve<ICreateGeneralInfoApplicationServices>();
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
