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

namespace Jmerp.Example.Customers.Middlewares.Services
{
    public interface IUpdateAddressApplicationServices
    {
        Task<ResponseResult> UpdateAddressSync(AddressDto address, CancellationToken cancellationToken);
    }

    public class UpdateAddressApplicationServices :  CustomerBasedServices,
        IUpdateAddressApplicationServices
    {
        public UpdateAddressApplicationServices(ICommandBus commandBus, IQueryProcessor queryProcessor)
                        : base(commandBus, queryProcessor)
        {
        }

        public async Task<ResponseResult> UpdateAddressSync(AddressDto address, CancellationToken cancellationToken)
        {
            var strErrors = new List<string>();
            var addressUpdate = AutoMapper.Mapper.Map<AddressDto, Address>(address);

            //validate address code
            strErrors.AddRange(AddressDetailSpecs.IsValidInput.WhyIsNotSatisfiedBy(addressUpdate));

            if (strErrors.Count > 0)
            {
                return ResponseResult.Failed(strErrors.ToArray());
            }

            var customerIdentity = addressUpdate.CustomerId;

            var customerQuery = await ReadCustomerModel(customerIdentity);
            var customerReadModel = customerQuery.ToList();

            if (customerReadModel?.FirstOrDefault()?.Id != customerIdentity)
                return ResponseResult.Failed("Customer is not found");

            var sourceId = await _commandBus.PublishAsync(
                new AddressUpdateCommand(customerIdentity, _commandSourceId, addressUpdate)
                ,cancellationToken).ConfigureAwait(false);

            customerQuery = await ReadCustomerModel(customerIdentity);
            customerReadModel = customerQuery.ToList();
            var latestAddressDetail = customerReadModel?.FirstOrDefault()?.AddressDetail;

            if (!latestAddressDetail.Addresses.Contains(addressUpdate))
                return ResponseResult.Failed("Failed update.");

            return ResponseResult.Succeed(
                AutoMapper.Mapper.Map<List<Customer>, List<CustomerDto>>(customerReadModel)
                );
        }
    }
}
