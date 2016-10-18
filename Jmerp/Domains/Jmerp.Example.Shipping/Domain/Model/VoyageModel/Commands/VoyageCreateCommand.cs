using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.ValueObjects;

namespace Jmerp.Example.Shipping.Domain.Model.VoyageModel.Commands
{
    public class VoyageCreateCommand : Command<VoyageAggregate, VoyageId>
    {
        public VoyageCreateCommand(
            VoyageId aggregateId,
            Schedule schedule) 
            : base(aggregateId)
        {
            Schedule = schedule;
        }

        public Schedule Schedule { get; }
    }

    public class VoyageCreateCommandHandler : CommandHandler<VoyageAggregate, VoyageId, VoyageCreateCommand>
    {
        public override Task ExecuteAsync(VoyageAggregate aggregate, VoyageCreateCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}
