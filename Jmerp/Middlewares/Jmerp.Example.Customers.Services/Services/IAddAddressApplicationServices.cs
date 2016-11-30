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
    public interface IAddAddressApplicationServices
    {
        Task<ResponseResult> AddAddressSync(List<AddressDto> addresses, CancellationToken cancellationToken);
    }

    public class AddAddressApplicationServices :  CustomerBasedServices, 
        IAddAddressApplicationServices
    {
        public AddAddressApplicationServices(ICommandBus commandBus, IQueryProcessor queryProcessor)
                        : base(commandBus, queryProcessor)
        {
        }

        public async Task<ResponseResult> AddAddressSync(List<AddressDto> addresses, CancellationToken cancellationToken)
        {
            var strErrors = new List<string>();
            var addressList = AutoMapper.Mapper.Map<List<AddressDto>, List<Address>>(addresses);

            //validate address code
            strErrors.AddRange(AddressDetailSpecs.IsAnyList.WhyIsNotSatisfiedBy(addressList));
            addressList.ForEach(a => {
                strErrors.AddRange(AddressDetailSpecs.IsValidInput.WhyIsNotSatisfiedBy(a));
            });

            if (strErrors.Count > 0)
            {
                return ResponseResult.Failed(strErrors.ToArray());
            }

            var customerIdentity = addressList.FirstOrDefault().CustomerId;

            var customerQuery = await ReadCustomerModel(customerIdentity);
            var customerReadModel = customerQuery.ToList();

            if (customerReadModel?.FirstOrDefault()?.Id != customerIdentity)
                return ResponseResult.Failed("Customer is not found");

            var addressDetail = new AddressDetail(addressList);

            var sourceId = await _commandBus.PublishAsync(
                new AddressAddCommand(customerIdentity, _commandSourceId, addressList)
                ,cancellationToken).ConfigureAwait(false);

            customerQuery = await ReadCustomerModel(customerIdentity);
            customerReadModel = customerQuery.ToList();
            var latestAddressDetail = customerReadModel?.FirstOrDefault()?.AddressDetail;

            if (!latestAddressDetail.Addresses.Contains(addressList.FirstOrDefault()))
                return ResponseResult.Failed("Failed created.");

            return ResponseResult.Succeed(
                AutoMapper.Mapper.Map<List<Customer>, List<CustomerDto>>(customerReadModel)
                );
        }
    }
}
