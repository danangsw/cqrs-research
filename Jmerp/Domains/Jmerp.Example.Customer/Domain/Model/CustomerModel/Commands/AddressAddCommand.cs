using EventFlow.Commands;
using System.Threading.Tasks;
using System.Threading;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using System;
using System.Collections.Generic;
using EventFlow.Core;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Commands
{
    public class AddressAddCommand : Command<CustomerAggregate, CustomerId>
    { 
        public AddressAddCommand(
            CustomerId aggregateId,
            ISourceId sourceId,
            List<Address> address)
            : base(aggregateId)
        {
            Address = address;
        }

        public List<Address> Address { get; }
    }

    public class AddressAddCommandHandler : CommandHandler<CustomerAggregate, CustomerId, AddressAddCommand>
    {
        public override Task ExecuteAsync(CustomerAggregate aggregate, AddressAddCommand command, CancellationToken cancellationToken)
        {
            aggregate.AddAddress(command.Address);
            return Task.FromResult(0);
        }
    }
}
