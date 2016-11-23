using EventFlow.Commands;
using Example.Shipping.Domain.Model.VoyageModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.VoyageModel.Commands
{
    public class VoyageCreateCommand : Command<VoyageAggregate, VoyageId>
    {
        public VoyageCreateCommand(
            VoyageId aggregateId)
            : base(aggregateId, CommandId.New)
        {

        }
    }

    public class VoyageCreateCommandHandler : CommandHandler<VoyageAggregate, VoyageId, VoyageCreateCommand>
    {
        public override Task ExecuteAsync(VoyageAggregate aggregate, VoyageCreateCommand command, CancellationToken cancellationToken)
        {
            aggregate.Create();

            return Task.FromResult(0);
        }
    }
}
