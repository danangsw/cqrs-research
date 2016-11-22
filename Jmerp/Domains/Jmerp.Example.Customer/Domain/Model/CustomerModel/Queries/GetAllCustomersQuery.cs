using EventFlow.Queries;
using System.Collections.Generic;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Queries
{
    public class GetAllCustomersQuery : IQuery<IReadOnlyCollection<Customer>>
    {
    }
}
