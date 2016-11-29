using NUnit.Framework;
using FluentAssertions;
using System.Threading.Tasks;
using Jmerp.Example.Customers.Middlewares.Tests.Mocks;
using System.Linq;
using Jmerp.Example.Customers.Middlewares.Models;
using System.Collections.Generic;
using System.Threading;
using Jmerp.Example.Customers.Middlewares.Services;
using Autofac;
using Jmerp.Middlewares.Boostrappers;

namespace Jmerp.Example.Customers.Middlewares.Tests.UnitTests.Services
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
            _container = _containerBuilder.BuildServices();
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
            var generalInfoUpdate = CustomerGeneralInfos.GeneralInfo_CS00002;

            //Act
            var servicesCreated =  _container.Resolve<ICreateGeneralInfoApplicationServices>();
            var responseCreate = await servicesCreated.CreateAsync(customer, CancellationToken.None);
            var responseCreateResult = ConvertResponse(responseCreate.Responses)?.ToList();

            var servicesUpdate = _container.Resolve<IUpdateGeneralInfoApplicationServices>();
            var responseUpdate = await servicesUpdate.UpdateAsync(
                customer.Id,
                generalInfoUpdate.OrganizationName,
                generalInfoUpdate.ContactPerson,
                generalInfoUpdate.Phone,
                generalInfoUpdate.Fax,
                generalInfoUpdate.Email,
                generalInfoUpdate.Web,
                CancellationToken.None);
            var responseUpdateResult = ConvertResponse(responseUpdate.Responses)?.ToList();

            var serviceAddAddress = _container.Resolve<IAddAddressApplicationServices>();
            var responseAddAddress = await serviceAddAddress.AddAddressSync(
                new List<AddressDto>() { CustomerAddressDetails.AddressDto_CS00001 },
                CancellationToken.None);
            var responseAddAddressResult = ConvertResponse(responseAddAddress.Responses)?.ToList();

            //Assert
            responseCreate.Succeeded.Should().BeTrue();
            responseCreate.Errors.Should().HaveCount(0);
            responseCreateResult.Should().BeOfType(typeof(List<CustomerDto>));

            responseUpdate.Succeeded.Should().BeTrue();
            responseUpdate.Errors.Should().HaveCount(0);
            responseUpdateResult.Should().BeOfType(typeof(List<CustomerDto>));

            responseAddAddress.Succeeded.Should().BeTrue();
            responseAddAddress.Errors.Should().HaveCount(0);
            responseAddAddressResult.Should().BeOfType(typeof(List<CustomerDto>));
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
