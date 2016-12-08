using EventFlow.Aggregates;
using EventFlow.EventStores;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Domain.Model.VoyageModel.Events
{
    [EventVersion("CarrierMovementUpdated", 1)]
    public class CarrierMovementUpdatedEvent : AggregateEvent<VoyageAggregate, VoyageId>
    {
        public CarrierMovementUpdatedEvent(
            CarrierMovement carrierMovement
        )
        {
            CarrierMovement = carrierMovement;
        }

        public CarrierMovement CarrierMovement { get; }
    }
}
