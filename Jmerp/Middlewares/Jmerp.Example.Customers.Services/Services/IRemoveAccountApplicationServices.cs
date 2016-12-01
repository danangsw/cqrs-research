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
    public interface IRemoveAccountApplicationServices
    {
        Task<ResponseResult> RemoveAsync(
            string customerId, List<string> accountIds, 
            CancellationToken cancellationToken);
    }

    public class RemoveAccountApplicationServices :  CustomerBasedServices,
        IRemoveAccountApplicationServices
    {
        public RemoveAccountApplicationServices(ICommandBus commandBus, IQueryProcessor queryProcessor)
                        : base(commandBus, queryProcessor)
        {
        }

        public async Task<ResponseResult> RemoveAsync(string customerId, List<string> accountIds, CancellationToken cancellationToken)
        {
            var strErrors = new List<string>();
            var accountIdList = AutoMapper.Mapper.Map<List<string>, List<AccountId>>(accountIds);
            var customerIdentity = AutoMapper.Mapper.Map<string, CustomerId>(customerId);

            //validate Ids
            strErrors.AddRange(AccountingDetailSpecs.IsNotNullOrEmptyInput.WhyIsNotSatisfiedBy(customerId));
            foreach (var item in accountIdList)
            {
                strErrors.AddRange(AccountingDetailSpecs.IsNotNullOrEmptyIdentity.WhyIsNotSatisfiedBy(item));
            }

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
               new AccountRemoveCommand(customerIdentity, _commandSourceId, accountIdList)
               , cancellationToken).ConfigureAwait(false);

            customerQuery = await ReadCustomerModel(customerIdentity);
            customerReadModel = customerQuery.ToList();
            var latestAccount = customerReadModel?.FirstOrDefault()?
                .AccountingDetail?.Accounts?.Select(a => a.Id);

            if (latestAccount.Intersect(accountIdList).Any())
                return ResponseResult.Failed(string.Format(CustomerMiddlewareMessageResources.MSG00004, accountIdList.ToString()));

            return ResponseResult.Succeed(
               AutoMapper.Mapper.Map<List<Customer>, List<CustomerDto>>(customerReadModel)
               );
        }
    }
}
