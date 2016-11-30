using System.Threading;
using System.Threading.Tasks;
using Jmerp.Example.Customers.Domain.Model.CustomerModel;
using Jmerp.Commons;
using EventFlow;
using Jmerp.Example.Customers.Middlewares.Models;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Commands;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Specifications;
using System.Collections.Generic;
using EventFlow.Queries;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Queries;
using System.Linq;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using System;
using EventFlow.Core;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Helpers;

namespace Jmerp.Example.Customers.Middlewares.Services
{
    public interface ISetAddressAsDefaultApplicationServices
    {
        Task<ResponseResult> SetAsDefaultShippingAddressSync(
            string customerId, string addressId, 
            CancellationToken cancellationToken);
        Task<ResponseResult> SetAsDefaultMailingAddressSync(
            string customerId, string addressId, 
            CancellationToken cancellationToken);
    }

    public class SetAddressAsDefaultApplicationServices :  CustomerBasedServices,
        ISetAddressAsDefaultApplicationServices
    {
        public SetAddressAsDefaultApplicationServices(ICommandBus commandBus, IQueryProcessor queryProcessor)
                        : base(commandBus, queryProcessor)
        {
        }

        public async Task<ResponseResult> SetAsDefaultMailingAddressSync(
            string customerId, string addressId, 
            CancellationToken cancellationToken)
        {
            return await SetAsDefaultSync(
                CustomerAddressTypeConstants.MailingAddress, 
                customerId, addressId, cancellationToken);
        }

        public async Task<ResponseResult> SetAsDefaultShippingAddressSync(
            string customerId, string addressId, 
            CancellationToken cancellationToken)
        {
            return await SetAsDefaultSync(
                CustomerAddressTypeConstants.ShippingAddress, 
                customerId, addressId, cancellationToken);
        }

        private async Task<ResponseResult> SetAsDefaultSync(
            string addressType, string customerId, 
            string addressId, CancellationToken cancellationToken)
        {
            var strErrors = new List<string>();
            var setAddressId = AutoMapper.Mapper.Map<string, AddressId>(addressId);
            var customerIdentity = AutoMapper.Mapper.Map<string, CustomerId>(customerId);

            //validate Ids
            strErrors.AddRange(AddressDetailSpecs.IsNotNullOrEmptyInput.WhyIsNotSatisfiedBy(customerId));
            strErrors.AddRange(AddressDetailSpecs.IsNotNullOrEmptyInput.WhyIsNotSatisfiedBy(addressId));

            if (strErrors.Count > 0)
            {
                return ResponseResult.Failed(strErrors.ToArray());
            }

            //get the customer by customerId
            var customerQuery = await ReadCustomerModel(customerIdentity);
            var customerReadModel = customerQuery.ToList();

            //validate customer
            if (customerReadModel?.FirstOrDefault()?.Id != customerIdentity)
                return ResponseResult.Failed("Customer is not found");

            var sourceId = await _commandBus.PublishAsync(
                new AddressAsDefaultSetCommand(customerIdentity, _commandSourceId, setAddressId, addressType)
                , cancellationToken).ConfigureAwait(false);

            customerQuery = await ReadCustomerModel(customerIdentity);
            customerReadModel = customerQuery.ToList();
            var latestAddress = customerReadModel?.FirstOrDefault()?
                .AddressDetail?.Addresses.Where(i => 
                i.Id.Value.Contains(setAddressId.Value)
                ).FirstOrDefault();

            if (!(latestAddress?.Id == setAddressId
                && latestAddress?.SetDefault == true
                && latestAddress?.AddressType == addressType))
                return ResponseResult.Failed("Failed created.");

            return ResponseResult.Succeed(
                AutoMapper.Mapper.Map<List<Customer>, List<CustomerDto>>(customerReadModel)
                );
        }
    }
}
