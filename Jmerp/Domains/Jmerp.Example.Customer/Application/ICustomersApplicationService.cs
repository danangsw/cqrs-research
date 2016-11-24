using Jmerp.Example.Customers.Domain.Model.CustomerModel;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace Jmerp.Example.Customers.Application
{
    public interface ICustomerApplicationService
    {
        Task CreateCustomerAsync(CustomerId customerId, GeneralInfo generalInfo, CancellationToken cancellationToken);
    }
}
