using EventFlow.Commands;
using System.Threading.Tasks;
using System.Threading;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using System;
using System.Collections.Generic;
using EventFlow.Core;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Commands
{
    public class AddressAsDefaultSetCommand : Command<CustomerAggregate, CustomerId>
    { 
        public AddressAsDefaultSetCommand(
            CustomerId aggregateId,
            ISourceId sourceId,
            AddressId addressId,
            string addressType
            ) : base(aggregateId)
        {
            AddressId = addressId;
            AddressType = addressType;
        }

        public AddressId AddressId { get; }
        public string AddressType { get; }
    }

    public class AddressAsDefaultSetCommandHandler : CommandHandler<CustomerAggregate, CustomerId, AddressAsDefaultSetCommand>
    {
        public override Task ExecuteAsync(CustomerAggregate aggregate, AddressAsDefaultSetCommand command, CancellationToken cancellationToken)
        {
            aggregate.SetAsDefault(command.AddressId, command.AddressType);
            return Task.FromResult(0);
        }
    }
}
