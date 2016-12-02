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
using System;

namespace Jmerp.Example.Customers.Middlewares.Services
{
    public interface IAddAccountApplicationServices
    {
        Task<ResponseResult> AddAsync(List<AccountDto> accounts, CancellationToken cancellationToken);
    }

    public class AddAccountApplicationServices :  CustomerBasedServices,
        IAddAccountApplicationServices
    {
        public AddAccountApplicationServices(ICommandBus commandBus, IQueryProcessor queryProcessor)
                        : base(commandBus, queryProcessor)
        {
        }
        
        public async Task<ResponseResult> AddAsync(List<AccountDto> accounts, CancellationToken cancellationToken)
        {
            var strErrors = new List<string>();
            var accountList = AutoMapper.Mapper.Map<List<AccountDto>, List<Account>>(accounts);

            //validate address code
            strErrors.AddRange(AccountingDetailSpecs.IsAnyList.WhyIsNotSatisfiedBy(accountList));
            accountList.ForEach(a => {
                strErrors.AddRange(AccountingDetailSpecs.IsValidInput.WhyIsNotSatisfiedBy(a));
            });

            if (strErrors.Count > 0)
            {
                return ResponseResult.Failed(strErrors.ToArray());
            }

            var customerIdentity = accountList.FirstOrDefault()?.CustomerId;

            var customerQuery = await ReadCustomerModel(customerIdentity);
            var customerReadModel = customerQuery.ToList();

            if (customerReadModel?.FirstOrDefault()?.Id != customerIdentity)
                return ResponseResult.Failed(string.Format(CustomerMiddlewareMessageResources.MSG00005, customerIdentity.Value));

            var sourceId = await _commandBus.PublishAsync(
                new AccountAddCommand(customerIdentity, _commandSourceId, accountList)
                , cancellationToken).ConfigureAwait(false);

            customerQuery = await ReadCustomerModel(customerIdentity);
            customerReadModel = customerQuery.ToList();
            var latestAccountingDetail = customerReadModel?.FirstOrDefault()?.AccountingDetail;

            if (!latestAccountingDetail.Accounts.Intersect(accountList).Any())
                return ResponseResult.Failed(string.Format(CustomerMiddlewareMessageResources.MSG00001, string.Join(",", accountList.Select(x => x.Id).ToList())));

            return ResponseResult.Succeed(
                AutoMapper.Mapper.Map<List<Customer>, List<CustomerDto>>(customerReadModel)
                );
        }
    }
}
