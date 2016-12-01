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
using System.Linq;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Helpers;
using System;
using Jmerp.Example.Customers.Middlewares.Resources;

namespace Jmerp.Example.Customers.Middlewares.Services
{
    public interface IRemoveAddressApplicationServices
    {
        Task<ResponseResult> RemoveAddressSync(
            string customerId, string addressId, 
            CancellationToken cancellationToken);
    }

    public class RemoveAddressApplicationServices :  CustomerBasedServices,
        IRemoveAddressApplicationServices
    {
        public RemoveAddressApplicationServices(ICommandBus commandBus, IQueryProcessor queryProcessor)
                        : base(commandBus, queryProcessor)
        {
        }

        public async Task<ResponseResult> RemoveAddressSync(
            string customerId, string addressId,
            CancellationToken cancellationToken)
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
                return ResponseResult.Failed(string.Format(CustomerMiddlewareMessageResources.MSG00005, customerIdentity.Value));

            var sourceId = await _commandBus.PublishAsync(
                new AddressRemoveCommand(customerIdentity, _commandSourceId, setAddressId)
                , cancellationToken).ConfigureAwait(false);

            customerQuery = await ReadCustomerModel(customerIdentity);
            customerReadModel = customerQuery.ToList();
            var latestAddress = customerReadModel?.FirstOrDefault()?
                .AddressDetail?.Addresses?.ToList();

            if (latestAddress.Any(a => a.Id == setAddressId))
                return ResponseResult.Failed(string.Format(CustomerMiddlewareMessageResources.MSG00004, setAddressId.Value));

            return ResponseResult.Succeed(
                AutoMapper.Mapper.Map<List<Customer>, List<CustomerDto>>(customerReadModel)
                );
        }
    }
}
