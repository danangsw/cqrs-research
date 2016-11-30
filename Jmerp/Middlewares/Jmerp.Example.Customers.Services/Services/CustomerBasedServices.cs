using EventFlow;
using EventFlow.Core;
using EventFlow.Queries;
using Jmerp.Commons;
using Jmerp.Example.Customers.Domain.Model.CustomerModel;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jmerp.Example.Customers.Middlewares.Services
{
    public class CustomerBasedServices
    {
        protected ICommandBus _commandBus;
        protected IQueryProcessor _queryProcessor;
        protected ISourceId _commandSourceId;

        public CustomerBasedServices(
            ICommandBus commandBus, 
            IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
            _commandSourceId = SourceId.New;
        }

        protected async Task<IReadOnlyCollection<Customer>> ReadCustomerModel(CustomerId id)
        {
            return await _queryProcessor.ProcessAsync(
                new GetCustomersQuery(new List<CustomerId> { id }), CancellationToken.None)
                .ConfigureAwait(false);
        }

        protected async Task<bool> IsCustomerIdExist(CustomerId id)
        {
            var customerQuery = await ReadCustomerModel(id);
            var customerReadModel = customerQuery.ToList();

            if (customerReadModel?.FirstOrDefault()?.Id != id)
                return false;
            return true;
        }
    }
}
