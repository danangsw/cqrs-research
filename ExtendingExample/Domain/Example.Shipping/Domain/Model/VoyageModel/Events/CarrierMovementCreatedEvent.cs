using EventFlow.Aggregates;
using EventFlow.EventStores;
using Example.Shipping.Domain.Model.VoyageModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.VoyageModel.Events
{
    [EventVersion("CarrierMovementCreated", 1)]
    public class CarrierMovementCreatedEvent : AggregateEvent<VoyageAggregate, VoyageId>
    {
        public CarrierMovementCreatedEvent(
            CarrierMovement carrierMovement
        )
        {
            CarrierMovement = carrierMovement;
        }

        public CarrierMovement CarrierMovement { get; }
    }
}
