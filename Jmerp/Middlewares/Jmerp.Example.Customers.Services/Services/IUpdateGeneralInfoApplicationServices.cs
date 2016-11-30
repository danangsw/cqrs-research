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
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;
using Jmerp.Example.Customers.Middlewares.Resources;

namespace Jmerp.Example.Customers.Middlewares.Services
{
    public interface IUpdateGeneralInfoApplicationServices
    {
        Task<ResponseResult> UpdateAsync(
            string customerId,
            string organizationName,
            string contactPerson,
            string phone,
            string fax,
            string email,
            string web,
            CancellationToken cancellationToken);
    }

    public class UpdateGeneralInfoApplicationServices : CustomerBasedServices, 
        IUpdateGeneralInfoApplicationServices
    {
        public UpdateGeneralInfoApplicationServices(ICommandBus commandBus, IQueryProcessor queryProcessor)
            :base(commandBus, queryProcessor)
        {
        }

        public async Task<ResponseResult> UpdateAsync(
            string customerId,
            string organizationName, 
            string contactPerson, 
            string phone, 
            string fax, 
            string email, 
            string web, 
            CancellationToken cancellationToken)
        {
            var strErrors = new List<string>();
            var generalInfo = new GeneralInfo(organizationName,contactPerson,phone,fax,email,web);

            //validate General info input
            strErrors.AddRange(GeneralInfoSpecs.IsValidInput.WhyIsNotSatisfiedBy(generalInfo));

            if (strErrors.Count > 0) return ResponseResult.Failed(strErrors.ToArray());

            var customerIdentity = new CustomerId(customerId);

            var customerQuery = await ReadCustomerModel(customerIdentity);
            var customerReadModel = customerQuery.ToList();

            if (customerReadModel?.FirstOrDefault()?.Id != customerIdentity)
                return ResponseResult.Failed(string.Format(CustomerMiddlewareMessageResources.MSG00005, customerIdentity.Value));

            await _commandBus.PublishAsync(
                new GeneralInfoUpdateCommand(customerIdentity, _commandSourceId, 
                organizationName, contactPerson, phone, fax, email, web), cancellationToken)
                .ConfigureAwait(false);

            customerQuery = await ReadCustomerModel(customerIdentity);
            customerReadModel = customerQuery.ToList();

            if (customerReadModel?.FirstOrDefault()?.GeneralInfo != generalInfo)
                return ResponseResult.Failed(string.Format(CustomerMiddlewareMessageResources.MSG00002, customerIdentity.Value));

            return ResponseResult.Succeed(
                AutoMapper.Mapper.Map<List<Customer>, List<CustomerDto>>(customerReadModel)
                );
        }
    }
}
