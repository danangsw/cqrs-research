using EventFlow.Queries;
using System.Collections.Generic;
using System.Linq;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Queries
{
    public class GetCustomersQuery : IQuery<IReadOnlyCollection<Customer>>
    {
        public GetCustomersQuery(
            IEnumerable<CustomerId> customerIds)
        {
            CustomerIds = customerIds.ToList();
        }

        public IReadOnlyCollection<CustomerId> CustomerIds { get; private set; }
    }
}
