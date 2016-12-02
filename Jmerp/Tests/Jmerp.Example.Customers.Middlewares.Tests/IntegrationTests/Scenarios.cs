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
            var responseAddAddress = await serviceAddAddress.AddAsync(
                new List<AddressDto>() { CustomerAddressDetails.AddressDto1_CS00001 },
                CancellationToken.None);
            var responseAddAddressResult = ConvertResponse(responseAddAddress.Responses)?.ToList();

            serviceAddAddress = _container.Resolve<IAddAddressApplicationServices>();
            responseAddAddress = await serviceAddAddress.AddAsync(
                new List<AddressDto>() { CustomerAddressDetails.AddressDto2_CS00001 },
                CancellationToken.None);
            responseAddAddressResult = ConvertResponse(responseAddAddress.Responses)?.ToList();

            serviceAddAddress = _container.Resolve<IAddAddressApplicationServices>();
            responseAddAddress = await serviceAddAddress.AddAsync(
                new List<AddressDto>() { CustomerAddressDetails.AddressDto3_CS00001 },
                CancellationToken.None);
            responseAddAddressResult = ConvertResponse(responseAddAddress.Responses)?.ToList();

            var addressUpdate = responseAddAddressResult.FirstOrDefault().AddressDetail.Addresses.FirstOrDefault();
            addressUpdate.AddressLine1 = "Jl. Kemenyan No.1";
            addressUpdate.PostalCode = "909090";

            var serviceUpdateAddress = _container.Resolve<IUpdateAddressApplicationServices>();
            var responseUpdateAddress = await serviceUpdateAddress.UpdateAsync(
                addressUpdate, CancellationToken.None);
            var responseUpdateAddressResult = ConvertResponse(responseUpdateAddress.Responses)?.ToList();

            serviceAddAddress = _container.Resolve<IAddAddressApplicationServices>();
            responseAddAddress = await serviceAddAddress.AddAsync(
                new List<AddressDto>() { CustomerAddressDetails.AddressDto4_CS00001 },
                CancellationToken.None);
            responseAddAddressResult = ConvertResponse(responseAddAddress.Responses)?.ToList();

            var addressFirst = responseAddAddressResult?.FirstOrDefault().AddressDetail?.Addresses?.FirstOrDefault();
            var addressLast = responseAddAddressResult?.FirstOrDefault().AddressDetail?.Addresses?.LastOrDefault();
            var addressDelete = responseAddAddressResult?.FirstOrDefault().AddressDetail?.Addresses?.FirstOrDefault();

            var serviceAsDefault = _container.Resolve<ISetAddressAsDefaultApplicationServices>();
            var responseSetAddress = await serviceAsDefault.SetAsDefaultShippingAddressAsync(
                addressFirst.CustomerId, addressFirst.Id, CancellationToken.None);
            var responseSetAddressResult = ConvertResponse(responseSetAddress.Responses)?.ToList();

            responseSetAddress = await serviceAsDefault.SetAsDefaultBillingAddressAsync(
                addressLast.CustomerId, addressLast.Id, CancellationToken.None);
            responseSetAddressResult = ConvertResponse(responseSetAddress.Responses)?.ToList();

            var serviceRemove = _container.Resolve<IRemoveAddressApplicationServices>();
            var responseRemoveAddress = await serviceRemove.RemoveAsync(
                addressDelete.CustomerId, addressDelete.Id, CancellationToken.None);
            var responseRemoveAddressResult = ConvertResponse(responseRemoveAddress.Responses)?.ToList();

            var serviceAccountsAdd = _container.Resolve<IAddAccountApplicationServices>();
            var responseAccountsAdd = await serviceAccountsAdd.AddAsync(
                new List<AccountDto> {
                    CustomerAccountingDetails.AccountDto1_CS00001,
                    CustomerAccountingDetails.AccountDto2_CS00001,
                    CustomerAccountingDetails.AccountDto3_CS00001,
                }, CancellationToken.None);
            var responseAccountsAddResult = ConvertResponse(responseAccountsAdd.Responses)?.ToList();

            var updateAccount = CustomerAccountingDetails.AccountDto2_CS00001;
            updateAccount.AccountBalance -= 555555;
            updateAccount.LastName = "Kadoor";
            var serviceAccountsUpdate = _container.Resolve<IUpdateAccountApplicationServices>();
            var responseAccountsUpdate = await serviceAccountsUpdate.UpdateAsync(
                updateAccount, CancellationToken.None);
            var responseAccountsUpdateResult = ConvertResponse(responseAccountsAdd.Responses)?.ToList();

            var serviceAccountsRemove = _container.Resolve<IRemoveAccountApplicationServices>();
            var responseAccountsRemove = await serviceAccountsRemove.RemoveAsync(
                CustomerAccountingDetails.AccountDto1_CS00001.CustomerId,
                new List<string> {
                    CustomerAccountingDetails.AccountDto1_CS00001.Id,
                    CustomerAccountingDetails.AccountDto3_CS00001.Id,
                }, CancellationToken.None);
            var responseAccountsRemoveResult = ConvertResponse(responseAccountsRemove.Responses)?.ToList();

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

            responseUpdateAddress.Succeeded.Should().BeTrue();
            responseUpdateAddress.Errors.Should().HaveCount(0);
            responseUpdateAddressResult.Should().BeOfType(typeof(List<CustomerDto>));

            responseSetAddress.Succeeded.Should().BeTrue();
            responseSetAddress.Errors.Should().HaveCount(0);
            responseSetAddressResult.Should().BeOfType(typeof(List<CustomerDto>));

            responseRemoveAddress.Succeeded.Should().BeTrue();
            responseRemoveAddress.Errors.Should().HaveCount(0);
            responseRemoveAddressResult.Should().BeOfType(typeof(List<CustomerDto>));

            responseAccountsAdd.Succeeded.Should().BeTrue();
            responseAccountsAdd.Errors.Should().HaveCount(0);
            responseAccountsAddResult.Should().BeOfType(typeof(List<CustomerDto>));

            responseAccountsUpdate.Succeeded.Should().BeTrue();
            responseAccountsUpdate.Errors.Should().HaveCount(0);
            responseAccountsUpdateResult.Should().BeOfType(typeof(List<CustomerDto>));

            responseAccountsRemove.Succeeded.Should().BeTrue();
            responseAccountsRemove.Errors.Should().HaveCount(0);
            responseAccountsRemoveResult.Should().BeOfType(typeof(List<CustomerDto>));
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
