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

namespace Jmerp.Example.Customers.Middlewares.Tests.IntegrationTests
{
    [Category("Integration")]
    public class GeneralInfoScenarios
    {
        [SetUp]
        public void Setup()
        {
            CustomerMiddlewaresBootstrapper.Build();
        }

        [TearDown]
        public void TearDown()
        {
            CustomerMiddlewaresBootstrapper.Dispose();
        }

        [Test]
        public async Task Simple()
        {
            //Arrange
            var customer = CustomerDtoList.Customer_CS00001;

            //Act
            var response = await CreateCustomerAggregateAsync(customer).ConfigureAwait(false);
            var responseResult = ConvertResponse(response.Responses)?.ToList();

            //Assert
            response.Succeeded.Should().BeTrue();
            response.Errors.Should().HaveCount(0);
            responseResult.Should().BeOfType(typeof(List<CustomerDto>));
        }

        private Task CreateCustomerAggregateBulkAsync()
        {
            return Task.WhenAll(CustomerDtoList.GetCustomers().Select(CreateCustomerAggregateAsync));
        }

        private Task<ResponseResult> CreateCustomerAggregateAsync(CustomerDto customer)
        {
            var service = CustomerMiddlewaresBootstrapper.GetService<CreateGeneralInfoApplicationServices>();
            return service.CreateAsync(customer, CancellationToken.None);
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
