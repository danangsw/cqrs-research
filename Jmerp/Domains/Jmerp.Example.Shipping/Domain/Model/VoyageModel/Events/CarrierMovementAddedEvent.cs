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
    [EventVersion("CarrierMovementCreated", 1)]
    public class CarrierMovementAddedEvent : AggregateEvent<VoyageAggregate, VoyageId>
    {
        public CarrierMovementAddedEvent(
            CarrierMovement carrierMovement
        )
        {
            CarrierMovement = carrierMovement;
        }

        public CarrierMovement CarrierMovement { get; }
    }
}
