using EventFlow.Commands;
using System.Threading.Tasks;
using System.Threading;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using System;
using System.Collections.Generic;
using EventFlow.Core;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Commands
{
    public class AddressRemoveCommand : Command<CustomerAggregate, CustomerId>
    { 
        public AddressRemoveCommand(
            CustomerId aggregateId,
            ISourceId sourceId,
            AddressId addressId)
            : base(aggregateId)
        {
            AddressId = addressId;
        }

        public AddressId AddressId { get; }
    }

    public class AddressRemoveCommandHandler : CommandHandler<CustomerAggregate, CustomerId, AddressRemoveCommand>
    {
        public override Task ExecuteAsync(CustomerAggregate aggregate, AddressRemoveCommand command, CancellationToken cancellationToken)
        {
            aggregate.RemoveAddress(command.AddressId);
            return Task.FromResult(0);
        }
    }
}
