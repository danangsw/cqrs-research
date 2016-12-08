using EventFlow.Commands;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Domain.Model.VoyageModel.Commands
{
    public class ScheduleCarrierMovementAddCommand : Command<VoyageAggregate, VoyageId>
    {
        public ScheduleCarrierMovementAddCommand(
            VoyageId aggregateId,
            CarrierMovement carrierMovement
            ) : base(aggregateId)
        {
            CarrierMovement = carrierMovement;
        }

        public CarrierMovement CarrierMovement { get; set; }
    }

    public class ScheduleCarrierMovementAddCommandHandler : CommandHandler<VoyageAggregate, VoyageId, ScheduleCarrierMovementAddCommand>
    {
        public override Task ExecuteAsync(VoyageAggregate aggregate, ScheduleCarrierMovementAddCommand command, CancellationToken cancellationToken)
        {
            aggregate.AddSheduleCarrierMovement(command.CarrierMovement);

            return Task.FromResult(0);
        }
    }
}
