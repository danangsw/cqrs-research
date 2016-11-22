using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Jmerp.Example.Customers.Domain.Model.CustomerModel;
using EventFlow;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Commands;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;

namespace Jmerp.Example.Customers.Application
{
    public class CustomerApplicationService : ICustomerApplicationService
    {
        private readonly ICommandBus _commandBus;
        public CustomerApplicationService(
            ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public Task CreateCustomerAsync(CustomerId customerId, GeneralInfo generalInfo, CancellationToken cancellationToken)
        {
            return _commandBus.PublishAsync(new CustomerCreateCommand(customerId, generalInfo), cancellationToken);
        }
    }
}
