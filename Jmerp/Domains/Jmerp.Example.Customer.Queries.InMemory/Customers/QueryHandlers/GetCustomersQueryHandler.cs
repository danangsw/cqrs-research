using EventFlow.Queries;
using Jmerp.Example.Customers.Domain.Model.CustomerModel;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using EventFlow.ReadStores.InMemory;

namespace Jmerp.Example.Customers.Queries.InMemory.Customers.QueryHandlers
{
    public class GetCustomersQueryHandler : IQueryHandler<GetCustomersQuery, IReadOnlyCollection<Customer>>
    {
        private readonly IInMemoryReadStore<CustomerReadModel> _readStore;

        public GetCustomersQueryHandler(
            IInMemoryReadStore<CustomerReadModel> readStore)
        {
            _readStore = readStore;
        }

        public async Task<IReadOnlyCollection<Customer>> ExecuteQueryAsync(GetCustomersQuery query, CancellationToken cancellationToken)
        {
            var customerIds = query.CustomerIds;
            var customerReadModels = await _readStore.FindAsync(rm => customerIds.Contains(rm.Id), cancellationToken).ConfigureAwait(false);
            return customerReadModels.Select(rm => rm.toCustomer()).ToList();
        }
    }
}

