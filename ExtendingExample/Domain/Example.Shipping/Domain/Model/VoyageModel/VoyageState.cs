using EventFlow.Aggregates;
using Example.Shipping.Domain.Model.VoyageModel.Events;
using Example.Shipping.Domain.Model.VoyageModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.VoyageModel
{
    public class VoyageState : AggregateState<VoyageAggregate, VoyageId, VoyageState>,
        IApply<VoyageCreatedEvent>,
        IApply<CarrierMovementAddedEvent>,
        IApply<CarrierMovementUpdatedEvent>,
        IApply<VoyageScheduleUpdatedEvent>
    {
        public Schedule Schedule { get; private set; }

        public void Apply(CarrierMovementUpdatedEvent aggregateEvent)
        {
            Schedule = Schedule.UpdateCarrierMovement(aggregateEvent.CarrierMovement);
        }

        public void Apply(CarrierMovementAddedEvent aggregateEvent)
        {
            Schedule = Schedule.AddCarrierMovement(aggregateEvent.CarrierMovement);
        }

        public void Apply(VoyageCreatedEvent aggregateEvent)
        {
            Schedule = new Schedule();
        }

        public void Apply(VoyageScheduleUpdatedEvent aggregateEvent)
        {
            Schedule = aggregateEvent.Schedule;
        }
    }
}
