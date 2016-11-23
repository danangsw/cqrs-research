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

namespace Jmerp.Example.Customers.Middlewares.Services
{
    public interface ICreateGeneralInfoApplicationServices
    {
        Task<ResponseResult> CreateAsync(CustomerDto customer, CancellationToken cancellationToken);
    }

    public class CreateGeneralInfoApplicationServices : ICreateGeneralInfoApplicationServices
    {
        private ICommandBus _commandBus;
        private IQueryProcessor _queryProcessor;

        public CreateGeneralInfoApplicationServices(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        public async Task<ResponseResult> CreateAsync(CustomerDto customer, CancellationToken cancellationToken)
        {
            var customerModel = AutoMapper.Mapper.Map<CustomerDto, Customer>(customer);
            var strErrors = new List<string>();

            //validate customer code
            strErrors.AddRange(CustomerSpecs.IsValidCode.WhyIsNotSatisfiedBy(customerModel.Id.Value));
            //validate General info input
            strErrors.AddRange(GeneralInfoSpecs.IsValidInput.WhyIsNotSatisfiedBy(customerModel.GeneralInfo));

            if (strErrors.Count > 0) return ResponseResult.Failed(strErrors.ToArray());

            await _commandBus.PublishAsync(
                new CustomerCreateCommand(customerModel.Id, customerModel.GeneralInfo), cancellationToken);

            var customerQuery = await _queryProcessor.ProcessAsync(
                new GetCustomersQuery(new List<CustomerId> { customerModel.Id }), CancellationToken.None)
                .ConfigureAwait(false);
            var customerReadModel = customerQuery.ToList();

            if (customerReadModel?.FirstOrDefault()?.Id != customerModel?.Id) return ResponseResult.Failed("Failed created.");

            var responseModel = AutoMapper.Mapper.Map<List<Customer>, List<CustomerDto>>(customerReadModel);

            return ResponseResult.Succeed(responseModel);
        }
    }
}
