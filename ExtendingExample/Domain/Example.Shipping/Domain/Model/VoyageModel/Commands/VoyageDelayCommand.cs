using EventFlow.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.VoyageModel.Commands
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
