using EventFlow.Commands;
using System.Threading.Tasks;
using System.Threading;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using System;
using System.Collections.Generic;
using EventFlow.Core;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Commands
{
    public class AddressUpdateCommand : Command<CustomerAggregate, CustomerId>
    { 
        public AddressUpdateCommand(
            CustomerId aggregateId,
            ISourceId sourceId,
            Address address)
            : base(aggregateId)
        {
            Address = address;
        }

        public Address Address { get; }
    }

    public class AddressUpdateCommandHandler : CommandHandler<CustomerAggregate, CustomerId, AddressUpdateCommand>
    {
        public override Task ExecuteAsync(CustomerAggregate aggregate, AddressUpdateCommand command, CancellationToken cancellationToken)
        {
            aggregate.UpdateAddress(command.Address);
            return Task.FromResult(0);
        }
    }
}
