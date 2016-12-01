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
using Jmerp.Example.Customers.Middlewares.Resources;

namespace Jmerp.Example.Customers.Middlewares.Services
{
    public interface IUpdateAccountApplicationServices
    {
        Task<ResponseResult> UpdateAsync(AccountDto account, CancellationToken cancellationToken);
    }

    public class UpdateAccountApplicationServices :  CustomerBasedServices,
        IUpdateAccountApplicationServices
    {
        public UpdateAccountApplicationServices(ICommandBus commandBus, IQueryProcessor queryProcessor)
                        : base(commandBus, queryProcessor)
        {
        }

        public async Task<ResponseResult> UpdateAsync(AccountDto account, CancellationToken cancellationToken)
        {
            var strErrors = new List<string>();
            var accountUpdate = AutoMapper.Mapper.Map<AccountDto, Account>(account);

            //validate address code
            strErrors.AddRange(AccountingDetailSpecs.IsValidInput.WhyIsNotSatisfiedBy(accountUpdate));

            if (strErrors.Count > 0)
            {
                return ResponseResult.Failed(strErrors.ToArray());
            }

            var customerIdentity = accountUpdate.CustomerId;

            var customerQuery = await ReadCustomerModel(customerIdentity);
            var customerReadModel = customerQuery.ToList();

            if (customerReadModel?.FirstOrDefault()?.Id != customerIdentity)
                return ResponseResult.Failed(string.Format(CustomerMiddlewareMessageResources.MSG00005, customerIdentity.Value));

            var sourceId = await _commandBus.PublishAsync(
                new AccountUpdateCommand(customerIdentity, _commandSourceId, accountUpdate)
                ,cancellationToken).ConfigureAwait(false);

            customerQuery = await ReadCustomerModel(customerIdentity);
            customerReadModel = customerQuery.ToList();
            var latestAccountDetail = customerReadModel?.FirstOrDefault()?.AccountingDetail;

            if (!latestAccountDetail.Accounts.Contains(accountUpdate))
                return ResponseResult.Failed(string.Format(CustomerMiddlewareMessageResources.MSG00002, accountUpdate.Id.Value));

            return ResponseResult.Succeed(
                AutoMapper.Mapper.Map<List<Customer>, List<CustomerDto>>(customerReadModel)
                );
        }
    }
}
