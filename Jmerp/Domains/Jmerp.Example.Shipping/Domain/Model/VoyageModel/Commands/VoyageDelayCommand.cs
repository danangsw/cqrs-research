using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace Jmerp.Example.Shipping.Domain.Model.VoyageModel.Commands
{
    public class VoyageDelayCommand : Command<VoyageAggregate, VoyageId>
    {
        public VoyageDelayCommand(
            VoyageId aggregateId,
            TimeSpan delay)
            : base(aggregateId)
        {
            Delay = delay;
        }

        public TimeSpan Delay { get; }
    }

    public class VoyageDelayCommandHandler : CommandHandler<VoyageAggregate, VoyageId, VoyageDelayCommand>
    {
        public override Task ExecuteAsync(VoyageAggregate aggregate, VoyageDelayCommand command, CancellationToken cancellationToken)
        {
            aggregate.Delay(command.Delay);
            return Task.FromResult(0);
        }
    }
}
