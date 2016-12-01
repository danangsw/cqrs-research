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
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;
using Jmerp.Example.Customers.Middlewares.Resources;

namespace Jmerp.Example.Customers.Middlewares.Services
{
    public interface IAddAddressApplicationServices
    {
        Task<ResponseResult> AddAsync(List<AddressDto> addresses, CancellationToken cancellationToken);
    }

    public class AddAddressApplicationServices :  CustomerBasedServices, 
        IAddAddressApplicationServices
    {
        public AddAddressApplicationServices(ICommandBus commandBus, IQueryProcessor queryProcessor)
                        : base(commandBus, queryProcessor)
        {
        }

        public async Task<ResponseResult> AddAsync(List<AddressDto> addresses, CancellationToken cancellationToken)
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
                return ResponseResult.Failed(string.Format(CustomerMiddlewareMessageResources.MSG00005, customerIdentity.Value));

            var sourceId = await _commandBus.PublishAsync(
                new AddressAddCommand(customerIdentity, _commandSourceId, addressList)
                ,cancellationToken).ConfigureAwait(false);

            customerQuery = await ReadCustomerModel(customerIdentity);
            customerReadModel = customerQuery.ToList();
            var latestAddressDetail = customerReadModel?.FirstOrDefault()?.AddressDetail;


            if (!latestAddressDetail.Addresses.Intersect(addressList).Any())
                return ResponseResult.Failed(string.Format(CustomerMiddlewareMessageResources.MSG00001, string.Join(",", addressList.Select(x => x.Id).ToList())));

            return ResponseResult.Succeed(
                AutoMapper.Mapper.Map<List<Customer>, List<CustomerDto>>(customerReadModel)
                );
        }
    }
}
